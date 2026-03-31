using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Get all PDF files in the current directory
        string[] pdfFiles = Directory.GetFiles(".", "*.pdf");
        foreach (string pdfPath in pdfFiles)
        {
            string fileName = Path.GetFileName(pdfPath);
            string outputFile = "processed_" + fileName;

            using (Document doc = new Document(pdfPath))
            {
                // Define a zero‑size rectangle (position is irrelevant for hidden fields)
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(0, 0, 0, 0);

                // Create a text box field named "SessionId"
                TextBoxField hiddenField = new TextBoxField(doc.Pages[1], rect)
                {
                    PartialName = "SessionId",
                    // The field is hidden by using a zero‑size rectangle; if the library version
                    // supports FieldFlags, you can also set: hiddenField.Flags = FieldFlags.Hidden;
                    Value = Guid.NewGuid().ToString()
                };

                // Add the hidden field to the document's form collection
                doc.Form.Add(hiddenField);

                // Save the modified PDF with a simple filename
                doc.Save(outputFile);
            }

            Console.WriteLine($"Processed {fileName} -> {outputFile}");
        }
    }
}
