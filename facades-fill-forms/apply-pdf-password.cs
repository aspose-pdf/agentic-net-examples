using System;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string protectedPath = "protected.pdf";
        const string userPassword = "user123";
        const string ownerPassword = "owner123";

        // Create a PDF document
        Document doc = new Document();
        Page page = doc.Pages.Add();
        TextFragment fragment = new TextFragment("This PDF is password protected.");
        page.Paragraphs.Add(fragment);

        // Apply password protection with desired permissions
        Permissions permissions = Permissions.PrintDocument; // allow printing; adjust as needed
        doc.Encrypt(userPassword, ownerPassword, permissions, CryptoAlgorithm.AESx128);

        // Save the protected PDF (guarded against missing GDI+ on non‑Windows platforms)
        SaveDocument(doc, protectedPath);
    }

    private static void SaveDocument(Document doc, string path)
    {
        // On Windows the native GDI+ library is always present.
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            doc.Save(path);
            Console.WriteLine($"PDF saved to '{path}'.");
            return;
        }

        // On macOS / Linux Aspose.Pdf may try to load libgdiplus. Handle the case where it is missing.
        try
        {
            doc.Save(path);
            Console.WriteLine($"PDF saved to '{path}'.");
        }
        catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
        {
            Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. PDF could not be saved.");
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