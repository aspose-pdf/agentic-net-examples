using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputTxt = "attachments_list.txt";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        try
        {
            // Load the PDF inside a using block for deterministic disposal
            using (Document doc = new Document(inputPdf))
            {
                // The EmbeddedFiles collection holds all file attachments (FileSpecification objects)
                var attachments = doc.EmbeddedFiles;

                // Prepare a list to hold attachment names
                var names = new System.Collections.Generic.List<string>();

                foreach (FileSpecification fileSpec in attachments)
                {
                    // FileSpecification.Name contains the attachment's file name
                    if (!string.IsNullOrEmpty(fileSpec.Name))
                        names.Add(fileSpec.Name);
                }

                // Write the names to a text file (one name per line)
                File.WriteAllLines(outputTxt, names);
            }

            Console.WriteLine($"Attachment list written to '{outputTxt}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}