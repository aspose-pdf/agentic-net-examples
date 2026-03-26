using System;
using System.IO;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf"; // existing PDF or will be created
        const string outputPdf = "output_pdfa3b.pdf";
        const string attachmentPath = "notes.txt";

        // Ensure the attachment file exists (create a sample if missing)
        if (!File.Exists(attachmentPath))
        {
            File.WriteAllText(attachmentPath, "Sample notes for PDF/A-3b attachment.");
        }

        // Load existing PDF or create a new one
        using (Document doc = File.Exists(inputPdf) ? new Document(inputPdf) : new Document())
        {
            // Ensure there is at least one page to host the annotation
            if (doc.Pages.Count == 0)
                doc.Pages.Add();

            // Create a file specification that points to the external text file
            FileSpecification fileSpec = new FileSpecification(attachmentPath);

            // Define the rectangle where the attachment icon will appear (coordinates are in points)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 120, 520);

            // Create the file‑attachment annotation on the first page
            FileAttachmentAnnotation attachment = new FileAttachmentAnnotation(doc.Pages[1], rect, fileSpec)
            {
                Name = "Notes",
                Contents = "Attached notes.txt"
            };

            // Add the annotation to the page
            doc.Pages[1].Annotations.Add(attachment);

            // Guard PDF/A‑3b conversion and saving against missing GDI+ (libgdiplus) on non‑Windows platforms
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                doc.Convert("conversion_log.xml", PdfFormat.PDF_A_3B, ConvertErrorAction.Delete);
                doc.Save(outputPdf);
            }
            else
            {
                try
                {
                    doc.Convert("conversion_log.xml", PdfFormat.PDF_A_3B, ConvertErrorAction.Delete);
                }
                catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
                {
                    Console.WriteLine("Warning: GDI+ (libgdiplus) not available – PDF/A conversion skipped.");
                }

                try
                {
                    doc.Save(outputPdf);
                }
                catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
                {
                    Console.WriteLine("Warning: GDI+ (libgdiplus) not available – PDF saving skipped.");
                }
            }
        }

        Console.WriteLine($"PDF/A‑3b document processing completed. Output path: '{outputPdf}'.");
    }

    private static bool ContainsDllNotFound(Exception? ex)
    {
        while (ex != null)
        {
            if (ex is DllNotFoundException)
                return true;
            ex = ex.InnerException;
        }
        return false;
    }
}
