using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main(string[] args)
    {
        // Input and output PDF file paths
        string inputPdf = "input.pdf";
        string outputPdf = "output.pdf";

        // Load the existing PDF document
        Document pdfDocument = new Document(inputPdf);

        // Get the first page (pages are 1‑based)
        Page page = pdfDocument.Pages[1];

        // Create a TextBuilder for the page and add a text fragment
        TextBuilder builder = new TextBuilder(page);
        TextFragment fragment = new TextFragment("Hello, Aspose.Pdf!");
        builder.AppendText(fragment);

        // Save the modified PDF document
        pdfDocument.Save(outputPdf);
    }
}