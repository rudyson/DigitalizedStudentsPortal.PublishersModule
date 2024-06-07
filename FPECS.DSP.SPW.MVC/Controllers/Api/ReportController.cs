using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using DocumentFormat.OpenXml;
using Microsoft.AspNetCore.Mvc;

namespace FPECS.DSP.SPW.MVC.Controllers.Api;

public class DocumentRequest
{
    public string Title { get; set; }
    public List<string> Publications { get; set; }
}

[Route("api/[controller]")]
[ApiController]
public class ReportController : ControllerBase
{
    [HttpPost]
    public IActionResult GenerateWord([FromBody] DocumentRequest request)
    {
        try
        {
            var fileName = $"GeneratedDocument_{Guid.NewGuid()}.docx";
            var filePath = Path.Combine("wwwroot","files", fileName);

            // Create the Word document in memory
            using (var wordDoc = WordprocessingDocument.Create(filePath, WordprocessingDocumentType.Document, true))
            {
                MainDocumentPart mainPart = wordDoc.AddMainDocumentPart();
                mainPart.Document = new Document();
                Body body = new Body();

                // Title
                Paragraph titleParagraph = new Paragraph();
                ParagraphProperties titleParagraphProperties = new ParagraphProperties
                {
                    Justification = new Justification() { Val = JustificationValues.Center }
                };
                titleParagraph.Append(titleParagraphProperties);
                Run titleRun = new Run();
                RunProperties titleRunProperties = new RunProperties
                {
                    Bold = new Bold(),
                    Caps = new Caps()
                };
                titleRun.Append(titleRunProperties);
                titleRun.Append(new Text(request.Title.ToUpper()));
                titleParagraph.Append(titleRun);
                body.Append(titleParagraph);

                // Publications List
                foreach (var publication in request.Publications)
                {
                    Paragraph publicationParagraph = new Paragraph();
                    Run publicationRun = new Run();
                    publicationRun.Append(new Text(publication));
                    publicationParagraph.Append(publicationRun);
                    body.Append(publicationParagraph);
                }

                mainPart.Document.Append(body);
                mainPart.Document.Save();
            }

            // Return the file path or URL
            var fileUrl = Url.Content($"~/wwwroot/{fileName}");
            return Ok(fileUrl);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.ToString());
        }
    }
}
