using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Open the PDF with read/write access so that we can write an incremental update back to the same file.
        using (FileStream stream = new FileStream(pdfPath, FileMode.Open, FileAccess.ReadWrite))
        {
            // Load the document from the stream.
            using (Document doc = new Document(stream))
            {
                // ----- Example modification: fill a text box field -----
                // The field name must be the fully qualified name as it appears in the PDF.
                const string fieldName = "FirstName";

                // Retrieve the field and cast it to a TextBoxField.
                if (doc.Form[fieldName] is TextBoxField textBox)
                {
                    textBox.Value = "John Doe"; // set the new value
                }

                // You can perform other form manipulations here (add fields, change appearances, etc.).

                // Save the document using the parameterless Save() method.
                // This writes the changes as an incremental update, preserving previous revisions.
                doc.Save();

                // Optional: verify that the document now has incremental updates.
                bool hasInc = doc.HasIncrementalUpdate();
                Console.WriteLine($"Incremental update applied: {hasInc}");
            }

            // The FileStream remains open until the Document is disposed.
            // After disposing the Document, the stream is still open; close it now.
        }

        Console.WriteLine("PDF saved with incremental update.");
    }
}