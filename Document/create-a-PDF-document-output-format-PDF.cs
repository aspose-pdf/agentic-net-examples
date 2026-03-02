using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Output PDF file path
        const string outputPath = "output.pdf";

        // Create a DocumentFactory instance (CreateDocument is an instance method)
        DocumentFactory factory = new DocumentFactory();

        // Use the factory to create an empty PDF document
        using (Document doc = factory.CreateDocument())
        {
            // Add a new page (first page is index 1)
            doc.Pages.Add();

            // Add a simple text fragment to the first page
            TextFragment tf = new TextFragment("Hello Aspose.Pdf!");
            doc.Pages[1].Paragraphs.Add(tf);

            // Save the document as PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF document created and saved to '{outputPath}'.");
    }
}