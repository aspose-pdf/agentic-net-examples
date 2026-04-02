using System;
using System.IO;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Text;

public class SetPdfPermissionsXml
{
    public static void Main()
    {
        // Create a simple XML file (for demonstration purposes only)
        string xmlFile = "sample.xml";
        string xmlContent = "<?xml version=\"1.0\" encoding=\"UTF-8\"?><root><paragraph>Hello, World!</paragraph></root>";
        File.WriteAllText(xmlFile, xmlContent);

        // Build PDF manually and add XML content as plain text
        using (Document pdfDocument = new Document())
        {
            Page page = pdfDocument.Pages.Add();

            TextFragment tf = new TextFragment(xmlContent);
            tf.TextState.FontSize = 12;
            tf.TextState.Font = FontRepository.FindFont("Arial");
            page.Paragraphs.Add(tf);

            // Define permissions – allow modifications but disallow printing and copying
            Permissions allowedPermissions = Permissions.ModifyContent |
                                                Permissions.ModifyTextAnnotations |
                                                Permissions.FillForm |
                                                Permissions.AssembleDocument;

            // Apply encryption with the custom permissions
            pdfDocument.Encrypt("userPassword", "ownerPassword", allowedPermissions, CryptoAlgorithm.AESx128);

            string outputPath = "output.pdf";

            // Guard Document.Save against missing GDI+ (libgdiplus) on non‑Windows platforms
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                pdfDocument.Save(outputPath);
                Console.WriteLine($"PDF saved to '{outputPath}'.");
            }
            else
            {
                try
                {
                    pdfDocument.Save(outputPath);
                    Console.WriteLine($"PDF saved to '{outputPath}'.");
                }
                catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
                {
                    Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. PDF was not saved.");
                }
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