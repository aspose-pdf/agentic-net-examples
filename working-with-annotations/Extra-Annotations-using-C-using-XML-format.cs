using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string xfdfPath = "annotations.xfdf";
        const string outputPdf = "output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF, add a text annotation, and export all annotations to XFDF (XML).
        using (Document doc = new Document(inputPdf))
        {
            // Create a rectangle for the annotation (fully qualified to avoid ambiguity).
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Add a TextAnnotation to the first page.
            TextAnnotation txtAnn = new TextAnnotation(doc.Pages[1], rect)
            {
                Title = "Note",
                Contents = "Extra annotation added programmatically.",
                Color = Aspose.Pdf.Color.Yellow
            };
            doc.Pages[1].Annotations.Add(txtAnn);

            // Export all annotations to an XFDF file.
            doc.ExportAnnotationsToXfdf(xfdfPath);
        }

        // Load the PDF again and import the previously exported XFDF annotations.
        using (Document newDoc = new Document(inputPdf))
        {
            newDoc.ImportAnnotationsFromXfdf(xfdfPath);
            // Save the document with the imported annotations.
            newDoc.Save(outputPdf);
        }

        Console.WriteLine($"Annotations exported to '{xfdfPath}' and imported into '{outputPdf}'.");
    }
}