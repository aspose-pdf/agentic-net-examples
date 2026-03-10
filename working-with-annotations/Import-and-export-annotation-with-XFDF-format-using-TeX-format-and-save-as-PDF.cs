using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Adjust these paths as needed
        string dataDir = "YOUR_DATA_DIRECTORY";
        string texFile = Path.Combine(dataDir, "input.tex");
        string pdfFile = Path.Combine(dataDir, "output.pdf");
        string xfdfFile = Path.Combine(dataDir, "annotations.xfdf");

        if (!File.Exists(texFile))
        {
            Console.Error.WriteLine($"TeX file not found: {texFile}");
            return;
        }

        // Load the TeX file into a PDF document
        TeXLoadOptions texLoadOptions = new TeXLoadOptions();
        using (Document doc = new Document(texFile, texLoadOptions))
        {
            // Add a sample text annotation on the first page
            Page page = doc.Pages[1];
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);
            TextAnnotation txtAnn = new TextAnnotation(page, rect)
            {
                Title = "Sample",
                Contents = "This is a sample annotation.",
                Color = Aspose.Pdf.Color.Yellow
            };
            page.Annotations.Add(txtAnn);

            // Export all annotations to an XFDF file
            doc.ExportAnnotationsToXfdf(xfdfFile);

            // Remove the annotation we just added (to demonstrate import)
            page.Annotations.Delete(page.Annotations.Count); // 1‑based indexing

            // Import annotations from the XFDF file back into the document
            doc.ImportAnnotationsFromXfdf(xfdfFile);

            // Save the final PDF
            doc.Save(pdfFile);
        }

        Console.WriteLine($"PDF saved to '{pdfFile}'. XFDF saved to '{xfdfFile}'.");
    }
}