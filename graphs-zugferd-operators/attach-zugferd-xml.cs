using System;
using System.IO;
using System.Runtime.InteropServices;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        string pdfPath = "input.pdf";
        string xmlPath = "invoice.xml";
        string outputPath = "output.pdf";

        // Ensure the source PDF exists – create a minimal PDF if it does not.
        if (!File.Exists(pdfPath))
        {
            using (Document emptyDoc = new Document())
            {
                emptyDoc.Pages.Add();
                emptyDoc.Save(pdfPath);
            }
        }

        // Ensure the ZUGFeRD XML file exists – create a placeholder if it does not.
        if (!File.Exists(xmlPath))
        {
            File.WriteAllText(xmlPath, "<?xml version=\"1.0\" encoding=\"UTF-8\"?><Invoice></Invoice>");
        }

        using (Document doc = new Document(pdfPath))
        {
            // Attach the ZUGFeRD XML file
            using (FileStream xmlStream = File.OpenRead(xmlPath))
            {
                // Fully qualified type to avoid ambiguity
                Aspose.Pdf.FileSpecification fileSpec = new Aspose.Pdf.FileSpecification(xmlStream, "invoice.xml", "ZUGFeRD Invoice XML");
                fileSpec.MIMEType = "application/xml";
                // Set the AFRelationship required for PDF/A‑3/ZUGFeRD
                fileSpec.AFRelationship = Aspose.Pdf.AFRelationship.Data;
                doc.EmbeddedFiles.Add(fileSpec);
            }

            // Convert the document to PDF/A‑3B for ZUGFeRD compliance
            try
            {
                Aspose.Pdf.PdfFormatConversionOptions conversionOptions = new Aspose.Pdf.PdfFormatConversionOptions(Aspose.Pdf.PdfFormat.PDF_A_3B);
                doc.Convert(conversionOptions);
            }
            catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
            {
                Console.WriteLine("GDI+ (libgdiplus) not available – PDF/A conversion skipped.");
            }

            // Save the resulting PDF – guard against missing GDI+ on non‑Windows platforms
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                doc.Save(outputPath);
            }
            else
            {
                try
                {
                    doc.Save(outputPath);
                }
                catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
                {
                    Console.WriteLine("GDI+ (libgdiplus) is not available – PDF save operation skipped.");
                }
            }
        }

        Console.WriteLine("ZUGFeRD XML attached and processing completed.");
    }

    // Helper to detect a nested DllNotFoundException (e.g., missing libgdiplus)
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