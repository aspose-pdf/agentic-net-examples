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

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdfPath))
        {
            // StringBuilder to accumulate note texts
            StringBuilder notesBuilder = new StringBuilder();

            // Iterate pages using 1‑based indexing (Aspose.Pdf requirement)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Annotations collection may be null; guard against it
                if (page.Annotations == null) continue;

                // Loop through all annotations on the page
                foreach (Annotation annotation in page.Annotations)
                {
                    // We are interested only in TextAnnotation (the "Note" type)
                    if (annotation is TextAnnotation note)
                    {
                        // Append the note's contents (if any) followed by a line break
                        if (!string.IsNullOrEmpty(note.Contents))
                        {
                            notesBuilder.AppendLine(note.Contents);
                        }
                    }
                }
            }

            // Write concatenated notes to a plain text file
            File.WriteAllText(outputTxtPath, notesBuilder.ToString());

            Console.WriteLine($"Extracted notes saved to '{outputTxtPath}'.");
        }
    }
}