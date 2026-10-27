using System;
using System.IO;
using System.Reflection;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Ensure the Aspose.PDF assembly can be located at runtime.
        // If you are using a project file, add the NuGet package "Aspose.PDF" (which provides Aspose.Pdf.dll).
        // The following handler helps when the assembly file is named differently (e.g., Aspose.Pdf.dll) on case‑sensitive platforms.
        AppDomain.CurrentDomain.AssemblyResolve += (sender, args) =>
        {
            if (args.Name.StartsWith("Aspose.PDF", StringComparison.OrdinalIgnoreCase))
            {
                string possiblePath = Path.Combine(AppContext.BaseDirectory, "Aspose.Pdf.dll");
                if (File.Exists(possiblePath))
                {
                    return Assembly.LoadFrom(possiblePath);
                }
            }
            return null;
        };

        const string inputPdf = "protected.pdf";
        const string ownerPassword = "owner123";
        const string outputTxt = "extracted.txt";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Verify that the Aspose.Pdf assembly is present before proceeding.
        string asposeDllPath = Path.Combine(AppContext.BaseDirectory, "Aspose.Pdf.dll");
        if (!File.Exists(asposeDllPath))
        {
            Console.Error.WriteLine($"Aspose.PDF assembly not found at '{asposeDllPath}'. Install the NuGet package 'Aspose.PDF' and ensure the DLL is copied to the output directory.");
            return;
        }

        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Supply the owner (or user) password **before** binding the PDF
            extractor.Password = ownerPassword;

            // Bind the PDF file (single‑argument overload)
            extractor.BindPdf(inputPdf);

            // Perform the extraction
            extractor.ExtractText();

            // Get the extracted text via the stream overload of GetText()
            using (MemoryStream textStream = new MemoryStream())
            {
                extractor.GetText(textStream);
                textStream.Position = 0;
                using (StreamReader reader = new StreamReader(textStream))
                {
                    string extractedText = reader.ReadToEnd();
                    File.WriteAllText(outputTxt, extractedText);
                }
            }
        }

        Console.WriteLine($"Text extracted to '{outputTxt}'.");
    }
}
