#pragma warning disable NU1902, NU1903

using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        // Ensure an input PDF exists – create a minimal one if missing.
        if (!File.Exists(inputPath))
        {
            using var placeholder = new Document();
            placeholder.Pages.Add();
            placeholder.Save(inputPath);
        }

        // Input PDF stream
        using (FileStream inputPdf = File.OpenRead(inputPath))
        // Temporary stream to hold the PDF after page deletion
        using (MemoryStream afterDeletion = new MemoryStream())
        {
            // -----------------------------------------------------------------
            // 1. Delete unwanted pages using Document API
            // -----------------------------------------------------------------
            int[] pagesToDelete = new[] { 2, 4 };
            Document pdfDoc = new Document(inputPdf);

            // Delete pages in descending order to keep indexes valid
            foreach (int page in pagesToDelete.OrderByDescending(p => p))
            {
                if (page >= 1 && page <= pdfDoc.Pages.Count)
                    pdfDoc.Pages.Delete(page);
            }

            // Save the intermediate PDF to a memory stream
            pdfDoc.Save(afterDeletion);
            afterDeletion.Position = 0;

            // -----------------------------------------------------------------
            // 2. Apply a 2‑up layout using PdfFileEditor.MakeNUp
            // -----------------------------------------------------------------
            // MakeNUp works with file paths, so write the intermediate stream to a
            // temporary file, run MakeNUp, then read the result back into a stream.
            string tempSource = Path.GetTempFileName();
            string tempResult = Path.GetTempFileName();

            try
            {
                // Write the intermediate PDF to the temporary source file
                File.WriteAllBytes(tempSource, afterDeletion.ToArray());

                PdfFileEditor editor = new PdfFileEditor();
                // false creates a portrait 2‑up layout
                editor.MakeNUp(new[] { tempSource }, tempResult, false);

                // Load the final PDF into a memory stream and write it out
                using var finalOutput = new MemoryStream(File.ReadAllBytes(tempResult));
                using var outFile = File.Create("output_2up.pdf");
                finalOutput.CopyTo(outFile);
            }
            finally
            {
                // Clean up temporary files
                if (File.Exists(tempSource)) File.Delete(tempSource);
                if (File.Exists(tempResult)) File.Delete(tempResult);
            }
        }
    }
}
#pragma warning restore NU1902, NU1903