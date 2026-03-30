using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations; // needed for StampAnnotation

class Program
{
    static void Main()
    {
        // List of PDF files to process – adjust as needed
        string[] pdfFiles = new string[] { "input1.pdf", "input2.pdf", "input3.pdf" };

        foreach (string inputPath in pdfFiles)
        {
            if (!File.Exists(inputPath))
            {
                Console.Error.WriteLine($"File not found: {inputPath}");
                continue;
            }

            string outputFileName = "repositioned_" + Path.GetFileNameWithoutExtension(inputPath) + ".pdf";

            using (Document doc = new Document(inputPath))
            {
                for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
                {
                    Page page = doc.Pages[pageIndex];

                    // Reposition each existing stamp on the current page
                    foreach (Annotation annotation in page.Annotations)
                    {
                        if (annotation is StampAnnotation stamp)
                        {
                            // Use the correct HorizontalAlignment enum (TextHorizontalAlignment does not exist)
                            stamp.HorizontalAlignment = HorizontalAlignment.Center;
                            // Keep the vertical alignment at the top of the page
                            stamp.VerticalAlignment = VerticalAlignment.Top;
                            // XIndent/YIndent are no longer available; default offsets are zero, which places the stamp
                            // exactly at the top‑center based on the alignment settings.
                        }
                    }
                }

                doc.Save(outputFileName);
                Console.WriteLine($"Processed '{inputPath}' -> '{outputFileName}'");
            }
        }
    }
}
