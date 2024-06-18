using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using FPECS.DSP.SPW.Business.Helpers;
using FPECS.DSP.SPW.DataAccess;
using FPECS.DSP.SPW.DataAccess.Entities.Enums;
using Microsoft.EntityFrameworkCore;

namespace FPECS.DSP.SPW.Business.Services;

public record InfoReportDocumentRow(int Number,string Name,string WorkType,string InputData,string Volume,string Collaborations);
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
            .Include(x => x.PublicationPublishers!)
                .ThenInclude(x => x.Pseudonym)
            .Include(x => x.PublicationExternalPublishers)
            .Where(x
                => x.PublicationPublishers!.Any(p => p.PublisherId == author.Id)
                   && x.Year >= currentYear.AddYears(-5))
            .OrderBy(x => x.Year)
            .ToListAsync(cancellationToken);

        var allPublications5YearsCount = publications.Count;

        var publicationsCategoryACount = publications.Where(x => x.Category is PublicationCategory.A).ToList().Count;
        var publicationsCategoryBCount = publications.Where(x => x.Category is PublicationCategory.B).ToList().Count;
        var publicationsCategoryCCount = publications.Where(x => x.Category is PublicationCategory.C).ToList().Count;

        var publicationsInternationalCount = publications.Where(x => x.IsInternational).ToList().Count;

        var publicationsManualsCount = publications.Where(x => x.Type is PublicationTypes.MethodicalManual or PublicationTypes.StudyMethodicalManual).ToList().Count;

        var statisticsDictionary = new Dictionary<string, int>
        {
            {"Усього публікацій за 5 років", allPublications5YearsCount},
            {"Публікацій категорії А", publicationsCategoryACount},
            {"Публікацій категорії Б", publicationsCategoryBCount},
            {"Публікацій категорії В", publicationsCategoryCCount},
            {"З них міжнародних публікацій", publicationsInternationalCount},
            {"Кількість друкованих посібників", publicationsManualsCount}
        };

        var infoReportRows = new List<InfoReportDocumentRow>();

        var iterator = 0;
        foreach (var publication in publications)
        {
            var authors = new List<string>();

            authors.AddRange(publication.PublicationPublishers!.Where(x => x.PublisherId!= author.Id).Select(x => x.Pseudonym!.ShortName));
            authors.AddRange(publication.PublicationExternalPublishers!.Select(x => x.Pseudonym));

            infoReportRows.Add(new (
                ++iterator,
                publication.Title,
                GetWorkType(publication.Type),
                publication.Reference,
                (publication.PagesAuthor ?? 0).ToString(),
                string.Join(", ", authors)));
        }

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
        authorRun.Append(new Text(ResearcherHelper.GetResearcherShortName(author).ToUpper()));
        authorParagraph.Append(authorRun);
        body.Append(authorParagraph);

        iterator = 0;

        foreach (var publication in publications)
        {
            Paragraph publicationParagraph = new Paragraph();
            Run publicationRun = new Run();
            publicationRun.Append(new Text($"{++iterator}. {publication.Reference}"));
            publicationParagraph.Append(publicationRun);
            body.Append(publicationParagraph);
        }

        InsertNewLine(body);
        InsertPublicationsTable(body, infoReportRows);

        InsertNewLine(body);
        InsertStatistics(statisticsDictionary, body);

        SetFontTimesNewRoman(body);

        mainPart.Document.Append(body);
        mainPart.Document.Save();

        return fileName;
    }

    private static void InsertPublicationsTable(Body body, List<InfoReportDocumentRow> rows)
    {
        Table table = new Table();

        TableProperties tableProperties = new TableProperties(
            new TableBorders(
                new TopBorder { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 4 },
                new BottomBorder { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 4 },
                new LeftBorder { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 4 },
                new RightBorder { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 4 },
                new InsideHorizontalBorder { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 4 },
                new InsideVerticalBorder { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 4 }
            )
        );
        table.AppendChild(tableProperties);

        TableRow headerRow = new TableRow();
        List<string> headers = new List<string>
            {
                "№ з/п", "Назва", "Характер роботи", "Вихідні дані", "Обсяг (у сторінках) / авторський доробок", "Співавтори"
            };
        foreach (string header in headers)
        {
            TableCell cell = new TableCell(new Paragraph(new Run(new Text(header))));
            headerRow.Append(cell);
        }
        table.Append(headerRow);

        foreach (var row in rows)
        {
            TableRow tableRow = new TableRow();

            tableRow.Append(new TableCell(new Paragraph(new Run(new Text(row.Number.ToString())))));
            tableRow.Append(new TableCell(new Paragraph(new Run(new Text(row.Name)))));
            tableRow.Append(new TableCell(new Paragraph(new Run(new Text(row.WorkType)))));
            tableRow.Append(new TableCell(new Paragraph(new Run(new Text(row.InputData)))));
            tableRow.Append(new TableCell(new Paragraph(new Run(new Text(row.Volume)))));
            tableRow.Append(new TableCell(new Paragraph(new Run(new Text(row.Collaborations)))));

            table.Append(tableRow);
        }

        body.Append(table);
    }

    private static string GetWorkType(PublicationTypes type)
    {
        return type switch
        {
            PublicationTypes.Article => "стаття",
            PublicationTypes.Theses => "тези",
            PublicationTypes.MethodicalManual => "Навчальний посібник",
            PublicationTypes.StudyMethodicalManual => "Навчально-методичний посібник",
            PublicationTypes.Patent => "патент",
            PublicationTypes.Notes => "замітка",
            _ => "невідомий",
        };
    }

    private static string GenerateReportName() => $"report_{Guid.NewGuid()}.docx";

    private static void InsertNewLine(Body body)
    {
        Paragraph publicationParagraph = new Paragraph();
        Run publicationRun = new Run();
        publicationRun.Append(new Text(""));
        publicationParagraph.Append(publicationRun);
        body.Append(publicationParagraph);
    }

    private static void InsertStatistics(Dictionary<string, int> statistics, Body body)
    {
        foreach (var keyValuePair in statistics)
        {
            Paragraph publicationParagraph = new Paragraph();
            Run publicationRun = new Run();
            publicationRun.Append(new Text($"{keyValuePair.Key}: {keyValuePair.Value}"));
            publicationParagraph.Append(publicationRun);
            body.Append(publicationParagraph);
        }
    }

    private static void SetFontTimesNewRoman(Body body)
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
                runProperties.FontSize = new FontSize() { Val = "28" };
            }
        }
    }
}