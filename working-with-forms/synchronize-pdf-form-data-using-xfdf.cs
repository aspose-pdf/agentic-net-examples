using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string sourcePdfPath = "sourceForm.pdf";
        const string targetPdfPath = "targetForm.pdf";
        const string outputPdfPath = "targetForm_updated.pdf";
        const string jsonDataPath   = "formData.json"; // using JSON as the supported interchange format

        // Verify source PDF exists
        if (!File.Exists(sourcePdfPath))
        {
            Console.Error.WriteLine($"Source PDF not found: {sourcePdfPath}");
            return;
        }

        // Verify target PDF exists
        if (!File.Exists(targetPdfPath))
        {
            Console.Error.WriteLine($"Target PDF not found: {targetPdfPath}");
            return;
        }

        // Export form data from the source PDF to a JSON file (Aspose.PDF provides JSON export/import)
        using (Document sourceDoc = new Document(sourcePdfPath))
        {
            // Enable automatic recalculation so that imported values are reflected correctly
            sourceDoc.Form.AutoRecalculate = true;

            using (FileStream jsonStream = new FileStream(jsonDataPath, FileMode.Create, FileAccess.Write))
            {
                sourceDoc.Form.ExportToJson(jsonStream);
            }
        }

        // Import the exported JSON into the target PDF and save the result
        using (Document targetDoc = new Document(targetPdfPath))
        {
            targetDoc.Form.AutoRecalculate = true;

            using (FileStream jsonStream = new FileStream(jsonDataPath, FileMode.Open, FileAccess.Read))
            {
                targetDoc.Form.ImportFromJson(jsonStream);
            }

            targetDoc.Save(outputPdfPath);
        }

        // Clean up the temporary JSON file
        if (File.Exists(jsonDataPath))
        {
            File.Delete(jsonDataPath);
        }

        Console.WriteLine($"Form data synchronized. Updated PDF saved to '{outputPdfPath}'.");
    }
}
