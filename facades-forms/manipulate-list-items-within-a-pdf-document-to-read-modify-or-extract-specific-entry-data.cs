using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_modified.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // ------------------------------------------------------------
        // 1. Extract all text from the PDF using PdfExtractor (facade API)
        // ------------------------------------------------------------
        string fullText;
        using (PdfExtractor extractor = new PdfExtractor())
        {
            extractor.BindPdf(inputPath);          // bind source PDF
            extractor.ExtractText();               // perform text extraction

            // Retrieve extracted text into a memory stream
            using (MemoryStream ms = new MemoryStream())
            {
                extractor.GetText(ms);
                fullText = Encoding.UTF8.GetString(ms.ToArray());
            }
        }

        // ------------------------------------------------------------
        // 2. Parse simple list items from the extracted text.
        //    This example treats lines that start with "-" or "1.", "2)" etc. as list entries.
        // ------------------------------------------------------------
        Console.WriteLine("List items found in the document:");
        string[] lines = fullText.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None);
        foreach (string line in lines)
        {
            string trimmed = line.TrimStart();
            bool isBullet = trimmed.StartsWith("- ");
            bool isNumbered = Regex.IsMatch(trimmed, @"^\d+[\.\)]\s");
            if (isBullet || isNumbered)
            {
                Console.WriteLine(trimmed);
            }
        }

        // ------------------------------------------------------------
        // 3. Modify a specific list entry using PdfContentEditor.
        //    Replace the first occurrence of a target string with a new value.
        // ------------------------------------------------------------
        const string targetText = "Old Item Text";        // text to find
        const string replacementText = "New Item Text";   // replacement

        PdfContentEditor editor = new PdfContentEditor();
        try
        {
            editor.BindPdf(inputPath);                     // bind source PDF
            editor.ReplaceText(targetText, replacementText); // replace text
            editor.Save(outputPath);                       // save modified PDF
        }
        finally
        {
            // Ensure resources are released
            editor.Close();
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}