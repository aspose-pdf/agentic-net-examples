using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Annotations;

class PdfDemo
{
    static void Main()
    {
        // -----------------------------------------------------------------
        // Paths used throughout the demo
        // -----------------------------------------------------------------
        const string inputPath = "sample.pdf";
        const string mergedPath = "merged.pdf";
        const string splitDir = "SplitPages";
        const string encryptedPath = "encrypted.pdf";
        const string decryptedPath = "decrypted.pdf";
        const string convertedPath = "converted.pdf";

        // -----------------------------------------------------------------
        // 1. Load a PDF document (using ensures deterministic disposal)
        // -----------------------------------------------------------------
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            // -----------------------------------------------------------------
            // 2. Basic document information
            // -----------------------------------------------------------------
            Console.WriteLine($"Pages: {doc.Pages.Count}");
            Console.WriteLine($"Title: {doc.Info.Title}");
            Console.WriteLine($"Author: {doc.Info.Author}");

            // -----------------------------------------------------------------
            // 3. Extract all text using TextAbsorber (recommended API)
            // -----------------------------------------------------------------
            TextAbsorber absorber = new TextAbsorber();
            absorber.ExtractionOptions = new TextExtractionOptions(TextExtractionOptions.TextFormattingMode.Pure);
            doc.Pages.Accept(absorber);
            Console.WriteLine($"Extracted text length: {absorber.Text.Length}");

            // -----------------------------------------------------------------
            // 4. Add a text annotation to the first page
            // -----------------------------------------------------------------
            // Pages are 1‑based in Aspose.Pdf
            Page firstPage = doc.Pages[1];
            // Fully qualified Rectangle avoids ambiguity with System.Drawing
            Aspose.Pdf.Rectangle annotRect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);
            TextAnnotation textAnn = new TextAnnotation(firstPage, annotRect)
            {
                Title = "Note",
                Contents = "Sample annotation added via Aspose.Pdf.",
                Color = Aspose.Pdf.Color.Yellow,
                Open = true,
                Icon = TextIcon.Note
            };
            firstPage.Annotations.Add(textAnn);

            // -----------------------------------------------------------------
            // 5. Save the modified document (PDF output does not require SaveOptions)
            // -----------------------------------------------------------------
            doc.Save("modified.pdf");
        }

        // -----------------------------------------------------------------
        // 6. Merge two PDFs (example merges the same file twice)
        // -----------------------------------------------------------------
        using (Document target = new Document(inputPath))
        using (Document source = new Document(inputPath))
        {
            target.Pages.Add(source.Pages);
            target.Save(mergedPath);
        }

        // -----------------------------------------------------------------
        // 7. Split a PDF into single‑page PDFs
        // -----------------------------------------------------------------
        Directory.CreateDirectory(splitDir);
        using (Document src = new Document(inputPath))
        {
            for (int i = 1; i <= src.Pages.Count; i++) // 1‑based loop
            {
                using (Document single = new Document())
                {
                    single.Pages.Add(src.Pages[i]);
                    string outPath = Path.Combine(splitDir, $"Page_{i}.pdf");
                    single.Save(outPath);
                }
            }
        }

        // -----------------------------------------------------------------
        // 8. Encrypt the PDF with AES‑256 and then decrypt it
        // -----------------------------------------------------------------
        const string userPwd = "user123";
        const string ownerPwd = "owner123";
        using (Document doc = new Document(inputPath))
        {
            Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;
            doc.Encrypt(userPwd, ownerPwd, perms, CryptoAlgorithm.AESx256);
            doc.Save(encryptedPath);
        }
        // Decrypt: open with user password, then call Decrypt() with no parameters
        using (Document encDoc = new Document(encryptedPath, userPwd))
        {
            encDoc.Decrypt();
            encDoc.Save(decryptedPath);
        }

        // -----------------------------------------------------------------
        // 9. Convert PDF to PDF/A‑1B (still a PDF output) and log conversion errors
        // -----------------------------------------------------------------
        using (Document doc = new Document(inputPath))
        {
            // Convert to PDF/A‑1B; errors are written to the specified log file
            doc.Convert("conversion_log.txt", PdfFormat.PDF_A_1B, ConvertErrorAction.Delete);
            doc.Save(convertedPath);
        }

        // -----------------------------------------------------------------
        // 10. Validate the resulting PDF/A‑1B document
        // -----------------------------------------------------------------
        using (Document doc = new Document(convertedPath))
        {
            bool isValid = doc.Validate("validation_log.txt", PdfFormat.PDF_A_1B);
            Console.WriteLine($"PDF/A‑1B validation result: {isValid}");
        }

        Console.WriteLine("Demo completed.");
    }
}