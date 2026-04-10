using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";

        // Ensure a PDF exists – if not, create a sample one with some custom metadata.
        if (!File.Exists(pdfPath))
        {
            CreateSamplePdf(pdfPath);
        }

        // Load the PDF document to access its Info dictionary (contains all metadata keys)
        using (Document doc = new Document(pdfPath))
        // Initialize PdfFileInfo facade to retrieve custom metadata values via GetMetaInfo
        using (PdfFileInfo pdfInfo = new PdfFileInfo(pdfPath))
        {
            // Extract keys that are not predefined (i.e., custom metadata)
            List<string> customKeys = doc.Info.Keys
                .Where(k => !DocumentInfo.IsPredefinedKey(k))
                .OrderBy(k => k)               // alphabetical order
                .ToList();

            // Display each custom key with its corresponding value
            foreach (string key in customKeys)
            {
                string value = pdfInfo.GetMetaInfo(key);
                Console.WriteLine($"{key}: {value}");
            }
        }
    }

    /// <summary>
    /// Creates a minimal PDF file and adds a few custom metadata entries.
    /// </summary>
    private static void CreateSamplePdf(string path)
    {
        using (Document doc = new Document())
        {
            // Add a blank page so the PDF is not empty.
            doc.Pages.Add();

            // Add custom metadata entries.
            doc.Info["Project"] = "Aspose Demo";
            doc.Info["Author"] = "John Doe"; // This is actually predefined, but kept for illustration.
            doc.Info["CustomTag1"] = "Value1";
            doc.Info["CustomTag2"] = "Value2";

            // Save the document.
            doc.Save(path);
        }
    }
}
