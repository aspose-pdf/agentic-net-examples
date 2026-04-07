using System;
using System.IO;
using System.Runtime.InteropServices;
using Aspose.Pdf;

public class Program
{
    public static void Main()
    {
        string inputPdfPath = "input.pdf";
        string zugferdXmlPath = "invoice.xml";
        string outputPdfPath = "encrypted.pdf";

        // Ensure the source PDF exists – create a minimal one if it does not.
        if (!File.Exists(inputPdfPath))
        {
            using (Document tempDoc = new Document())
            {
                // Add a blank page so the PDF is valid.
                tempDoc.Pages.Add();
                tempDoc.Save(inputPdfPath);
            }
        }

        // Ensure the ZUGFeRD XML exists – create a minimal placeholder if it does not.
        if (!File.Exists(zugferdXmlPath))
        {
            string minimalXml = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>" +
                                 "<rsm:CrossIndustryDocument xmlns:rsm=\"urn:ferd:CrossIndustryDocument:invoice:1p0\"/>";
            File.WriteAllText(zugferdXmlPath, minimalXml);
        }

        // Load the existing PDF document
        using (Document document = new Document(inputPdfPath))
        {
            // Embed the ZUGFeRD XML as an attached file (PDF/A‑3 requirement)
            FileSpecification fileSpec = new FileSpecification(zugferdXmlPath, "invoice.xml");
            fileSpec.MIMEType = "application/xml";
            fileSpec.AFRelationship = Aspose.Pdf.AFRelationship.Data;
            fileSpec.Contents = new MemoryStream(File.ReadAllBytes(zugferdXmlPath));
            document.EmbeddedFiles.Add(fileSpec);

            // Convert the document to PDF/A‑3B for ZUGFeRD compliance (guarded for non‑Windows platforms)
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                PdfFormatConversionOptions conversionOptions = new PdfFormatConversionOptions(PdfFormat.PDF_A_3B);
                document.Convert(conversionOptions);
            }
            else
            {
                Console.WriteLine("PDF/A‑3 conversion requires GDI+ which is unavailable on this platform. Skipping conversion.");
            }

            // Encrypt the PDF with user and owner passwords and appropriate permissions
            Permissions permissions = Permissions.PrintDocument | Permissions.ExtractContent;
            document.Encrypt("userPass123", "ownerPass123", permissions, CryptoAlgorithm.AESx256);

            // Save the encrypted PDF – guard against missing libgdiplus on non‑Windows platforms
            try
            {
                document.Save(outputPdfPath);
                Console.WriteLine($"ZUGFeRD PDF encrypted and saved as {outputPdfPath}");
            }
            catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
            {
                Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. The PDF could not be saved.");
                // Optionally, you could fallback to another library or inform the user.
            }
        }
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
