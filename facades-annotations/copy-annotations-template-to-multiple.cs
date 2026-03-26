using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string templatePath = "template.pdf";
        string[] targetPaths = new string[] { "target1.pdf", "target2.pdf" };

        if (!File.Exists(templatePath))
        {
            Console.Error.WriteLine("Template file not found: " + templatePath);
            return;
        }

        // Export annotations from the template PDF to an in‑memory XFDF stream
        using (PdfAnnotationEditor templateEditor = new PdfAnnotationEditor())
        {
            templateEditor.BindPdf(templatePath);
            using (MemoryStream xfdfStream = new MemoryStream())
            {
                templateEditor.ExportAnnotationsToXfdf(xfdfStream);
                // Prepare the stream for reading
                xfdfStream.Position = 0;

                foreach (string targetPath in targetPaths)
                {
                    if (!File.Exists(targetPath))
                    {
                        Console.Error.WriteLine("Target file not found: " + targetPath);
                        continue;
                    }

                    using (PdfAnnotationEditor targetEditor = new PdfAnnotationEditor())
                    {
                        targetEditor.BindPdf(targetPath);
                        // Reset stream position before each import
                        xfdfStream.Position = 0;
                        targetEditor.ImportAnnotationsFromXfdf(xfdfStream);
                        string outputFileName = Path.GetFileNameWithoutExtension(targetPath) + "_with_annots.pdf";
                        targetEditor.Save(outputFileName);
                        Console.WriteLine("Annotations copied to: " + outputFileName);
                    }
                }
            }
        }
    }
}