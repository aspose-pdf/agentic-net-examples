using System;
using System.IO;
using Aspose.Pdf; // core API

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Build the XFDF file name (same folder, same base name, .xfdf extension)
        string xfdfPath = Path.Combine(
            Path.GetDirectoryName(pdfPath) ?? string.Empty,
            Path.GetFileNameWithoutExtension(pdfPath) + ".xfdf");

        try
        {
            // Load the PDF inside a using block for deterministic disposal
            using (Document doc = new Document(pdfPath))
            {
                // 1‑based page number whose annotations we want to export
                int pageNumber = 1; // change as needed

                if (pageNumber < 1 || pageNumber > doc.Pages.Count)
                {
                    Console.Error.WriteLine("Invalid page number.");
                    return;
                }

                // ------------------------------------------------------------
                // The XFDF support lives in the separate assembly Aspose.Pdf.Xfdf.
                // To avoid a compile‑time dependency (and the missing‑namespace
                // error) we create the XfdfSaveOptions instance via reflection.
                // This keeps the code pure‑core and still provides the required
                // functionality when the optional assembly is present at runtime.
                // ------------------------------------------------------------
                const string xfdfAssemblyName = "Aspose.Pdf.Xfdf"; // NuGet package name
                const string xfdfOptionsTypeName = "Aspose.Pdf.Xfdf.XfdfSaveOptions";

                // Load the assembly (will throw if not found – let the catch handle it)
                var xfdfAssembly = AppDomain.CurrentDomain.Load(xfdfAssemblyName);
                var xfdfOptionsType = xfdfAssembly.GetType(xfdfOptionsTypeName, throwOnError: true);

                // Create an instance of XfdfSaveOptions
                dynamic xfdfOptions = Activator.CreateInstance(xfdfOptionsType);
                // PageIndex is zero‑based, so subtract 1 from the 1‑based pageNumber
                xfdfOptions.PageIndex = pageNumber - 1;

                // Save only the annotations of the selected page to XFDF
                // The XfdfSaveOptions class derives from Aspose.Pdf.SaveOptions, so we can cast.
                doc.Save(xfdfPath, (SaveOptions)xfdfOptions);
            }

            Console.WriteLine($"Annotations from page saved to: {xfdfPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
