using System;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string outputPath = "expire.pdf";

        // Define the expiration date (e.g., December 31, 2025)
        DateTime expirationDate = new DateTime(2025, 12, 31);
        // Format the date for JavaScript (MM/dd/yyyy)
        string jsDate = expirationDate.ToString("MM/dd/yyyy");

        // JavaScript that closes the document if the current date is past the expiration date
        string script = $"if (new Date() > new Date('{jsDate}')) {{ this.closeDoc(); }}";

        // Create a new PDF document (no external input file required)
        Document pdfDoc = new Document();
        // Add at least one blank page so the document is not empty
        pdfDoc.Pages.Add();

        // Embed the JavaScript as an OpenAction
        pdfDoc.OpenAction = new JavascriptAction(script);

        // Save the result
        pdfDoc.Save(outputPath);
    }
}
