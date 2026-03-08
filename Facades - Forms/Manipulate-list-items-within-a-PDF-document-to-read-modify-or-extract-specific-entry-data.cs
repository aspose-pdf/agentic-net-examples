using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string listItemToFind = "Item 2";          // text to locate in the list
        const string newText = "Item 2 - Updated";      // replacement text

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // ------------------------------------------------------------
        // 1. Extract all text from the PDF using PdfExtractor (Facade)
        // ------------------------------------------------------------
        PdfExtractor extractor = new PdfExtractor();
        extractor.BindPdf(inputPath);
        extractor.ExtractText();

        using (MemoryStream textStream = new MemoryStream())
        {
            extractor.GetText(textStream);
            string allText = Encoding.UTF8.GetString(textStream.ToArray());

            // ------------------------------------------------------------
            // 2. Parse lines to locate list items (simple detection)
            // ------------------------------------------------------------
            string[] lines = allText.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None);
            List<string> modifiedLines = new List<string>();
            bool madeChange = false;

            foreach (string line in lines)
            {
                string trimmed = line.TrimStart();

                // Detect bullet or numbered list items
                bool isListItem = trimmed.StartsWith("-") ||
                                  Regex.IsMatch(trimmed, @"^\d+[\.\)]\s");

                if (isListItem && trimmed.Contains(listItemToFind))
                {
                    // Replace the matching line with the new text
                    modifiedLines.Add(newText);
                    madeChange = true;
                }
                else
                {
                    modifiedLines.Add(line);
                }
            }

            // ------------------------------------------------------------
            // 3. If a matching item was found, replace it in the PDF
            // ------------------------------------------------------------
            if (madeChange)
            {
                // Use PdfContentEditor to replace the old text with the new text
                PdfContentEditor editor = new PdfContentEditor();
                editor.BindPdf(inputPath);
                // ReplaceText overload replaces all occurrences of the old string
                editor.ReplaceText(listItemToFind, newText);
                // Save the modified document
                editor.Save(outputPath);
                Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
            }
            else
            {
                Console.WriteLine("No matching list item found; no changes made.");
            }
        }

        // ------------------------------------------------------------
        // 4. Example: Extract and display all list items (read‑only)
        // ------------------------------------------------------------
        Console.WriteLine("\nExtracted list items:");
        PdfExtractor listExtractor = new PdfExtractor();
        listExtractor.BindPdf(File.Exists(outputPath) ? outputPath : inputPath);
        listExtractor.ExtractText();

        using (MemoryStream listStream = new MemoryStream())
        {
            listExtractor.GetText(listStream);
            string listText = Encoding.UTF8.GetString(listStream.ToArray());
            string[] listLines = listText.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None);

            foreach (string line in listLines)
            {
                string trimmed = line.TrimStart();
                if (trimmed.StartsWith("-") ||
                    Regex.IsMatch(trimmed, @"^\d+[\.\)]\s"))
                {
                    Console.WriteLine(trimmed);
                }
            }
        }
    }
}