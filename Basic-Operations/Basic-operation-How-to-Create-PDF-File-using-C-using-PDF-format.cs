using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Create a new PDF document
        Aspose.Pdf.Document pdfDocument = new Aspose.Pdf.Document();

        // Add a blank page to the document
        Aspose.Pdf.Page page = pdfDocument.Pages.Add();

        // Create a text fragment with the desired content
        Aspose.Pdf.Text.TextFragment textFragment = new Aspose.Pdf.Text.TextFragment("Hello, Aspose.PDF!");

        // Set font, size and color using fully qualified types
        textFragment.TextState.Font = Aspose.Pdf.Text.FontRepository.FindFont("Helvetica");
        textFragment.TextState.FontSize = 12;
        textFragment.TextState.ForegroundColor = Aspose.Pdf.Color.Black;

        // Position the text on the page (coordinates are in points)
        textFragment.Position = new Aspose.Pdf.Text.Position(100, 700);

        // Append the text fragment to the page using TextBuilder
        Aspose.Pdf.Text.TextBuilder textBuilder = new Aspose.Pdf.Text.TextBuilder(page);
        textBuilder.AppendText(textFragment);

        // Save the PDF document to a file
        pdfDocument.Save("output.pdf");
    }
}