using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Work on a copy so the original file remains unchanged
        File.Copy(inputPath, outputPath, true);

        // Load the PDF document
        Document pdfDoc = new Document(outputPath);

        // Iterate through all pages and their annotations
        foreach (Page page in pdfDoc.Pages)
        {
            foreach (Annotation annot in page.Annotations)
            {
                // The Open flag exists only on specific annotation types (e.g., TextAnnotation, PopupAnnotation).
                // Cast to the appropriate derived type before setting the property.
                switch (annot)
                {
                    case TextAnnotation textAnn:
                        textAnn.Open = true;
                        break;
                    case PopupAnnotation popupAnn:
                        popupAnn.Open = true;
                        break;
                    // Add other annotation types that expose an Open property if needed.
                }
            }
        }

        // Save the modified document
        pdfDoc.Save(outputPath);

        Console.WriteLine($"All applicable annotation Open flags set to true and saved to '{outputPath}'.");
    }
}
