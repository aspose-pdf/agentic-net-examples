using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF and look for an embedded file whose name ends with "_old.pdf".
        string nameToRemove = null;
        using (Document doc = new Document(inputPdf))
        {
            if (doc.EmbeddedFiles != null && doc.EmbeddedFiles.Count > 0)
            {
                // Aspose collections are 1‑based.
                for (int i = 1; i <= doc.EmbeddedFiles.Count; i++)
                {
                    FileSpecification fileSpec = doc.EmbeddedFiles[i];
                    if (!string.IsNullOrEmpty(fileSpec.Name) &&
                        fileSpec.Name.EndsWith("_old.pdf", StringComparison.OrdinalIgnoreCase))
                    {
                        nameToRemove = fileSpec.Name;
                        break; // remove the first match
                    }
                }
            }

            if (nameToRemove == null)
            {
                // No matching attachment – simply copy the original file.
                doc.Save(outputPdf);
                Console.WriteLine("No matching attachment found; original PDF saved unchanged.");
                return;
            }

            // Remove the identified embedded file by its name.
            doc.EmbeddedFiles.Delete(nameToRemove);
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Attachment '{nameToRemove}' removed. Output saved to '{outputPdf}'.");
    }
}
