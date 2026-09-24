using System;
using System.IO;
using System.Text;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputTxtPath = "notes.txt";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Use a using block for deterministic disposal of the Document.
        using (Document doc = new Document(inputPdfPath))
        {
            StringBuilder notesBuilder = new StringBuilder();

            // Aspose.Pdf uses 1‑based page indexing.
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Iterate over all annotations on the page.
                foreach (Annotation annotation in page.Annotations)
                {
                    // TextAnnotation represents a "note" (sticky‑note) annotation.
                    if (annotation is TextAnnotation note)
                    {
                        // Append the note's contents. Trim to remove extra whitespace.
                        notesBuilder.AppendLine(note.Contents?.Trim());
                    }
                }
            }

            // Write the concatenated notes to a plain text file.
            File.WriteAllText(outputTxtPath, notesBuilder.ToString());
        }

        Console.WriteLine($"All note texts have been saved to '{outputTxtPath}'.");
    }
}