using System;
using System.Text;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Example XML content to be converted
        const string xmlContent = "<root><message>Hello, PDF!</message></root>";
        // Destination PDF file
        const string outputPath = "output.pdf";

        // Create a new PDF document with default settings
        Document pdfDoc = new Document();
        // Add a page to the document
        Page page = pdfDoc.Pages.Add();
        // Insert the XML content as plain text (you can format it further if needed)
        TextFragment tf = new TextFragment(xmlContent);
        page.Paragraphs.Add(tf);

        // Save the document as PDF using default settings
        pdfDoc.Save(outputPath);

        Console.WriteLine($"PDF created at '{outputPath}'.");
    }
}
