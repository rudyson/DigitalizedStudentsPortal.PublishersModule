using DocumentFormat.OpenXml.Office2016.Excel;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using DocumentFormat.OpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FPECS.DSP.SPW.DataAccess;
using FPECS.DSP.SPW.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace FPECS.DSP.SPW.Business.Services;

public interface IReportService
{
    Task<string?> GeneratePublicationsListFor5Years(string email, CancellationToken cancellationToken = default);
    Task<string?> GenerateSimplePublicationsList(string email, CancellationToken cancellationToken = default);
}
public class ReportService(ApplicationDbContext context) : IReportService
{
    public Task<string?> GeneratePublicationsListFor5Years(string email, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<string?> GenerateSimplePublicationsList(string email, CancellationToken cancellationToken = default)
    {
        var fileName = GenerateReportName();
        var filePath = Path.Combine("wwwroot", "files", "reports", fileName);

        var author = await context.Researchers.AsNoTracking()
            .FirstOrDefaultAsync(x => x.Email == email, cancellationToken);

        if (author is null)
        {
            return null;
        }

        var currentDate = DateTime.Now;
        var currentYear = DateOnly.FromDateTime(currentDate);

        var publications = await context.Publications.AsNoTracking()
            .Include(x => x.PublicationPublishers)
            .Where(x
                => x.PublicationPublishers!.Any(p => p.PublisherId == author.Id)
                   && x.Year >= currentYear.AddYears(-5))
            .ToListAsync(cancellationToken);


        using var wordDoc = WordprocessingDocument.Create(filePath, WordprocessingDocumentType.Document, true);
        MainDocumentPart mainPart = wordDoc.AddMainDocumentPart();
        mainPart.Document = new Document();
        Body body = new Body();

        Paragraph titleParagraph = new Paragraph();
        ParagraphProperties justifyCenterParagraphProperties = new ParagraphProperties
        {
            Justification = new Justification() { Val = JustificationValues.Center }
        };
        titleParagraph.Append(justifyCenterParagraphProperties);

        Run titleRun = new Run();
        RunProperties boldCapsRunProperties = new RunProperties
        {
            Bold = new Bold(),
            Caps = new Caps()
        };
        titleRun.Append(boldCapsRunProperties);
        titleRun.Append(new Text("Список публікацій".ToUpper()));
        titleParagraph.Append(titleRun);
        body.Append(titleParagraph);

        Paragraph authorParagraph = new Paragraph();
        authorParagraph.Append(justifyCenterParagraphProperties.CloneNode(true));
        Run authorRun = new Run();
        authorRun.Append(boldCapsRunProperties.CloneNode(true));
        authorRun.Append(new Text(GetResearcherShortName(author).ToUpper()));
        authorParagraph.Append(authorRun);
        body.Append(authorParagraph);

        foreach (var publication in publications)
        {
            Paragraph publicationParagraph = new Paragraph();
            Run publicationRun = new Run();
            publicationRun.Append(new Text(publication.Reference));
            publicationParagraph.Append(publicationRun);
            body.Append(publicationParagraph);
        }

        SetFontTimesNewRoman(body);

        mainPart.Document.Append(body);
        mainPart.Document.Save();

        return fileName;
    }

    private static string GenerateReportName() => $"report_{Guid.NewGuid()}.docx";

    private static string GetResearcherShortName(Researcher researcher)
    {
        var result = new StringBuilder(researcher.LastName);
        result.Append($" {researcher.FirstName.ToUpper()[0]}.");
        if (!string.IsNullOrEmpty(researcher.MiddleName))
        {
            result.Append($" {researcher.MiddleName.ToUpper()[0]}.");
        }
        return result.ToString();
    }

    static void SetFontTimesNewRoman(Body body)
    {
        foreach (Paragraph paragraph in body.Descendants<Paragraph>())
        {
            foreach (Run run in paragraph.Descendants<Run>())
            {
                RunProperties? runProperties = run.RunProperties;
                if (runProperties == null)
                {
                    runProperties = new RunProperties();
                    run.RunProperties = runProperties;
                }

                runProperties.RunFonts = new RunFonts() { Ascii = "Times New Roman" };
                runProperties.FontSize = new FontSize() { Val = "28" }; // 14 pt = 28 half-point size
            }
        }
    }
}