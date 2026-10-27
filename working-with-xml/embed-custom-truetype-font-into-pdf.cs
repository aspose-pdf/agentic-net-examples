using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // ---------------------------------------------------------------------
        // Load the Aspose.Pdf assembly manually to avoid a runtime FileNotFoundException.
        // This is useful when the DLL is present in the executable directory but the
        // CLR cannot locate it automatically (e.g., case‑sensitive file systems).
        // ---------------------------------------------------------------------
        string baseDir = AppDomain.CurrentDomain.BaseDirectory;
        string[] possibleNames = { "Aspose.Pdf.dll", "Aspose.PDF.dll" };
        string assemblyPath = null;
        foreach (var name in possibleNames)
        {
            var candidate = Path.Combine(baseDir, name);
            if (File.Exists(candidate))
            {
                assemblyPath = candidate;
                break;
            }
        }
        if (assemblyPath == null)
        {
            Console.Error.WriteLine("Aspose.Pdf assembly not found in the application directory.");
            return;
        }
        // Load the assembly explicitly.
        Assembly.LoadFrom(assemblyPath);

        // ---------------------------------------------------------------------
        // Paths – adjust as needed
        // ---------------------------------------------------------------------
        const string xmlPath   = "input.xml";
        const string ttfPath   = "CustomFont.ttf";
        const string outputPdf = "output.pdf";

        // Verify input files exist
        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }
        if (!File.Exists(ttfPath))
        {
            Console.Error.WriteLine($"TrueType font file not found: {ttfPath}");
            return;
        }

        // Load the XML content into a PDF document
        using (Document doc = new Document(xmlPath, new XmlLoadOptions()))
        {
            // Open the custom TrueType font – this loads the font and marks it for embedding
            Font customFont = FontRepository.OpenFont(ttfPath);

            // Iterate over all pages and apply the custom font to every TextFragment
            // Aspose.Pdf uses 1‑based page indexing
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Use LINQ's OfType<T>() to safely get only TextFragment objects
                foreach (TextFragment tf in page.Paragraphs.OfType<TextFragment>())
                {
                    tf.TextState.Font = customFont; // font will be embedded on save
                }
            }

            // Save the resulting PDF – no SaveOptions needed because the target is PDF
            doc.Save(outputPdf);
        }

        // Ensure all native resources are released before the process ends.
        // This helps avoid the "cannot open AsposePdfApi_*.dll" lock when the
        // assembly is re‑compiled immediately after execution.
        GC.Collect();
        GC.WaitForPendingFinalizers();

        Console.WriteLine($"PDF generated with embedded font: {outputPdf}");
    }
}
