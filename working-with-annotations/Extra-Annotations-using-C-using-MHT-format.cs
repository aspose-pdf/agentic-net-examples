using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string mhtPath = "input.mht";
        const string pdfPath = "output.pdf";
        const string xfdfPath = "annotations.xfdf";

        if (!File.Exists(mhtPath))
        {
            Console.Error.WriteLine($"MHT file not found: {mhtPath}");
            return;
        }

        // Load the MHT file using MhtLoadOptions
        MhtLoadOptions loadOptions = new MhtLoadOptions();

        using (Document doc = new Document(mhtPath, loadOptions))
        {
            // Ensure the document has at least one page
            if (doc.Pages.Count == 0)
            {
                Console.Error.WriteLine("The loaded document contains no pages.");
                return;
            }

            // Add a TextAnnotation to the first page
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);
            TextAnnotation txtAnn = new TextAnnotation(doc.Pages[1], rect)
            {
                Title = "Extra Note",
                Contents = "This annotation was added after converting from MHT.",
                Color = Aspose.Pdf.Color.Yellow
            };
            doc.Pages[1].Annotations.Add(txtAnn);

            // Save the modified document as PDF
            doc.Save(pdfPath);

            // Export all annotations to an XFDF file
            doc.ExportAnnotationsToXfdf(xfdfPath);
        }

        Console.WriteLine($"PDF saved to '{pdfPath}'.");
        Console.WriteLine($"Annotations exported to '{xfdfPath}'.");
    }
}