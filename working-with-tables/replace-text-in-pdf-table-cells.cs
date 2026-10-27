using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string keyword = "OldValue";      // text to search for
        const string replacement = "NewValue";  // new text to place

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // ----- OPTIONAL: remove any embedded files (fixed according to verified fix) -----
                if (doc.EmbeddedFiles != null && doc.EmbeddedFiles.Count > 0)
                {
                    for (int i = doc.EmbeddedFiles.Count; i >= 1; i--)
                    {
                        var fileSpec = doc.EmbeddedFiles[i];
                        if (fileSpec != null && !string.IsNullOrEmpty(fileSpec.Name))
                        {
                            doc.EmbeddedFiles.Delete(fileSpec.Name);
                        }
                    }
                }

                // Iterate over all pages (Aspose.Pdf uses 1‑based indexing)
                for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
                {
                    Page page = doc.Pages[pageNum];

                    // Look for tables on the page
                    foreach (var paragraph in page.Paragraphs)
                    {
                        if (paragraph is Table table)
                        {
                            // Iterate through rows and cells
                            foreach (Row row in table.Rows)
                            {
                                foreach (Cell cell in row.Cells)
                                {
                                    bool containsKeyword = false;

                                    // Check each paragraph inside the cell for the keyword
                                    foreach (var cellParagraph in cell.Paragraphs)
                                    {
                                        // Most common case: TextFragment
                                        if (cellParagraph is TextFragment tf && tf.Text != null && tf.Text.Contains(keyword))
                                        {
                                            containsKeyword = true;
                                            break;
                                        }
                                        // If other paragraph types are present, attempt to extract their textual representation via ToString()
                                        string paraText = cellParagraph?.ToString();
                                        if (!string.IsNullOrEmpty(paraText) && paraText.Contains(keyword))
                                        {
                                            containsKeyword = true;
                                            break;
                                        }
                                    }

                                    if (containsKeyword)
                                    {
                                        // Remove existing content
                                        cell.Paragraphs.Clear();

                                        // Create a new TextFragment with the replacement text
                                        TextFragment newFragment = new TextFragment(replacement);

                                        // (Optional) Preserve basic formatting from the first old paragraph if needed
                                        // Here we simply use default formatting; you can copy Font, FontSize, Color, etc.

                                        cell.Paragraphs.Add(newFragment);
                                    }
                                }
                            }
                        }
                    }
                }

                // Save the modified PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"Keyword replacement completed. Saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
