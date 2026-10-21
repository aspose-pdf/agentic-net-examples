using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

class BatchAnnotationCleaner
{
    static void Main(string[] args)
    {
        // Expect: args[0] = folder containing PDFs, args[1] = age in days
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: BatchAnnotationCleaner <folderPath> <days>");
            return;
        }

        string folderPath = args[0];
        if (!Directory.Exists(folderPath))
        {
            Console.Error.WriteLine($"Folder not found: {folderPath}");
            return;
        }

        if (!int.TryParse(args[1], out int days))
        {
            Console.Error.WriteLine("Invalid days value.");
            return;
        }

        DateTime cutoffDate = DateTime.Now.AddDays(-days);

        foreach (string pdfFile in Directory.GetFiles(folderPath, "*.pdf"))
        {
            string tempFile = Path.Combine(folderPath,
                Path.GetFileNameWithoutExtension(pdfFile) + "_cleaned.pdf");

            // Load the PDF to inspect annotations (Document must be disposed)
            using (Document doc = new Document(pdfFile))
            {
                List<string> namesToDelete = new List<string>();

                // Pages are 1‑based in Aspose.Pdf
                for (int i = 1; i <= doc.Pages.Count; i++)
                {
                    Page page = doc.Pages[i];
                    foreach (Annotation annot in page.Annotations)
                    {
                        // Modified is a non‑nullable DateTime, so use it directly.
                        DateTime modDate = annot.Modified;

                        if (modDate < cutoffDate)
                        {
                            // The editor deletes annotations by their Name (or empty string if not set).
                            // If Name is null, we cannot delete it via the editor, so we skip it.
                            if (!string.IsNullOrEmpty(annot.Name))
                                namesToDelete.Add(annot.Name);
                        }
                    }
                }

                if (namesToDelete.Count > 0)
                {
                    // Use Facade API to delete annotations by Name
                    using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
                    {
                        editor.BindPdf(pdfFile);

                        foreach (string name in namesToDelete)
                            editor.DeleteAnnotation(name);

                        // Save the cleaned PDF
                        editor.Save(tempFile);
                    }
                }
                else
                {
                    // No annotations to remove; copy original
                    File.Copy(pdfFile, tempFile, true);
                }
            }

            // Replace original file with cleaned version
            File.Delete(pdfFile);
            File.Move(tempFile, pdfFile);
            Console.WriteLine($"Processed: {Path.GetFileName(pdfFile)}");
        }
    }
}
