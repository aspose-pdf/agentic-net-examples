using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main(string[] args)
    {
        // Expect one or more PDF file paths as command‑line arguments.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Please provide PDF file paths as arguments.");
            return;
        }

        foreach (string pdfPath in args)
        {
            if (!File.Exists(pdfPath))
            {
                Console.Error.WriteLine($"File not found: {pdfPath}");
                continue;
            }

            try
            {
                // Load the PDF document (lifecycle rule: use Document constructor inside a using block).
                using (Document doc = new Document(pdfPath))
                {
                    // ---------- Fonts ----------
                    // Fonts are stored per‑page in the Resources.Fonts collection.
                    // We collect distinct font names across all pages.
                    var fontNames = new HashSet<string>();
                    foreach (Page page in doc.Pages)
                    {
                        if (page.Resources?.Fonts != null)
                        {
                            foreach (var font in page.Resources.Fonts)
                            {
                                // FontName is the reliable property for the font's name.
                                string name = font.FontName;
                                if (!string.IsNullOrEmpty(name))
                                    fontNames.Add(name);
                            }
                        }
                    }
                    int fontCount = fontNames.Count;

                    // ---------- Tables ----------
                    // Tables appear as Aspose.Pdf.Table objects inside page.Paragraphs.
                    int tableCount = 0;
                    foreach (Page page in doc.Pages)
                    {
                        foreach (var paragraph in page.Paragraphs)
                        {
                            if (paragraph is Table) // Table is in Aspose.Pdf namespace
                                tableCount++;
                        }
                    }

                    // ---------- Form fields ----------
                    // Document.Form provides access to the form; Fields is the collection of form fields.
                    int formFieldCount = 0;
                    if (doc.Form != null && doc.Form.Fields != null)
                        formFieldCount = doc.Form.Fields.Count();

                    // Log the statistics for the current document.
                    Console.WriteLine($"Document: {System.IO.Path.GetFileName(pdfPath)}");
                    Console.WriteLine($"  Fonts: {fontCount}");
                    Console.WriteLine($"  Tables: {tableCount}");
                    Console.WriteLine($"  Form fields: {formFieldCount}");
                }
            }
            catch (Exception ex)
            {
                // Report any errors encountered while processing the file.
                Console.Error.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
            }
        }
    }
}
