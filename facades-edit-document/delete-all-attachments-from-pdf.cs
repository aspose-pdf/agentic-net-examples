using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text; // Added for TextFragment

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_no_attachments.pdf";

        // Ensure a source PDF exists – create a simple PDF with an embedded file if missing
        if (!File.Exists(inputPath))
        {
            // Creating a PDF on non‑Windows platforms requires libgdiplus. Guard the operation.
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                CreateSamplePdfWithAttachment(inputPath);
            }
            else
            {
                Console.WriteLine("Cannot create a sample PDF on this platform because GDI+ (libgdiplus) is missing.");
                Console.WriteLine("Please provide an existing PDF at '{0}' or run the program on Windows.", inputPath);
                return;
            }
        }

        // Delete all attachments using PdfContentEditor
        var editor = new PdfContentEditor();
        editor.BindPdf(inputPath);
        editor.DeleteAttachments();

        // Guard Save on non‑Windows platforms – either try‑catch the GDI+ exception or skip.
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            editor.Save(outputPath);
        }
        else
        {
            try
            {
                editor.Save(outputPath);
            }
            catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
            {
                Console.WriteLine("Warning: GDI+ (libgdiplus) is not available – the PDF was not saved on this platform.");
                return;
            }
        }

        // Verify that no attachments remain using PdfExtractor
        var extractor = new PdfExtractor();
        extractor.BindPdf(outputPath);
        List<FileSpecification> attachments = extractor.GetAttachmentInfo();

        if (attachments.Count == 0)
        {
            Console.WriteLine("All attachments have been successfully deleted. Attachment count = 0.");
        }
        else
        {
            Console.WriteLine($"Attachment deletion failed. Remaining attachments count = {attachments.Count}.");
        }
    }

    private static void CreateSamplePdfWithAttachment(string path)
    {
        // Create a simple PDF document
        var doc = new Document();
        var page = doc.Pages.Add();
        page.Paragraphs.Add(new TextFragment("Sample PDF with attachment"));

        // Create a temporary file to embed
        string tempFile = Path.GetTempFileName();
        File.WriteAllText(tempFile, "This is the content of the embedded file.");

        // Create a FileSpecification for the embedded file
        var fileSpec = new FileSpecification(tempFile, "EmbeddedSample.txt");
        // Load the file bytes into the Contents stream (required for PDF/A compliance)
        fileSpec.Contents = new MemoryStream(File.ReadAllBytes(tempFile));

        // Add the file specification to the document's EmbeddedFiles collection
        doc.EmbeddedFiles.Add(fileSpec);

        // Guard the Save call – on non‑Windows platforms it may throw a GDI+ related exception.
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            doc.Save(path);
        }
        else
        {
            try
            {
                doc.Save(path);
            }
            catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
            {
                Console.WriteLine("Warning: GDI+ (libgdiplus) is not available – the PDF was not saved on this platform.");
                // Re‑throw or simply exit because we cannot continue without a PDF.
                throw new InvalidOperationException("Unable to create sample PDF on this platform.", ex);
            }
        }

        // Clean up the temporary file
        File.Delete(tempFile);
    }

    // Helper that walks the inner‑exception chain looking for a DllNotFoundException (libgdiplus).
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
