using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades; // Facades namespace required by the task

class Program
{
    static void Main()
    {
        // Configuration switch: set to true to disable BaseFolder injection (e.g., in test environments)
        bool disableBaseFolder = true;

        const string htmlInputPath = "input.html";
        const string pdfOutputPath = "output.pdf";

        if (!File.Exists(htmlInputPath))
        {
            Console.Error.WriteLine($"HTML source not found: {htmlInputPath}");
            return;
        }

        // Load HTML with optional BaseFolder injection
        HtmlLoadOptions loadOptions = new HtmlLoadOptions();

        // NOTE: In recent versions of Aspose.PDF the HtmlLoadOptions class no longer exposes a
        // "BaseFolder" property.  The property was removed in favour of automatic resolution of
        // relative resources based on the HTML file location.  Therefore we simply omit the
        // assignment when the switch is disabled.  If a future version re‑introduces the property,
        // the conditional block can be re‑enabled without affecting the rest of the code.
        if (!disableBaseFolder)
        {
            // When BaseFolder is enabled in older SDK versions, set it to the directory of the HTML file.
            // The property is guarded by a compile‑time check to avoid errors on newer SDKs.
#if NETFRAMEWORK || NETCOREAPP3_1 || NET5_0_OR_GREATER
            // The property exists in older SDKs – use reflection to set it safely.
            var prop = typeof(HtmlLoadOptions).GetProperty("BaseFolder");
            if (prop != null && prop.CanWrite)
            {
                prop.SetValue(loadOptions, Path.GetDirectoryName(Path.GetFullPath(htmlInputPath)));
            }
#endif
        }

        // Use a using block for deterministic disposal of the Document
        using (Document doc = new Document(htmlInputPath, loadOptions))
        {
            // Save the resulting PDF
            doc.Save(pdfOutputPath);
        }

        Console.WriteLine($"PDF generated at '{pdfOutputPath}'. BaseFolder injection {(disableBaseFolder ? "disabled" : "enabled")}." );
    }
}
