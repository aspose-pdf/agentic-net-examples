using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string sourcePdfPath = "source.pdf";      // PDF containing the form fields to copy
        const string destinationPdfPath = "destination.pdf"; // PDF that will receive the fields
        const string outputPdfPath = "output.pdf";      // Resulting PDF

        // Verify that the input files exist
        if (!File.Exists(sourcePdfPath) || !File.Exists(destinationPdfPath))
        {
            Console.Error.WriteLine("Source or destination PDF not found.");
            return;
        }

        try
        {
            // Prepare the output file – start with a copy of the destination PDF
            File.Copy(destinationPdfPath, outputPdfPath, overwrite: true);

            // Load the source PDF (the one that has the form fields)
            Document srcDoc = new Document(sourcePdfPath);
            // Load the output PDF (a copy of the destination) where fields will be added
            Document outDoc = new Document(outputPdfPath);

            // Iterate over each form field in the source and clone it into the destination
            foreach (Field srcField in srcDoc.Form.Fields)
            {
                // Use the Clone method (available on Field) to create an independent copy.
                // The returned object is of type object, so cast it back to Field.
                Field clonedField = srcField.Clone() as Field;
                if (clonedField != null)
                {
                    outDoc.Form.Add(clonedField);
                }
            }

            // Save the modified document
            outDoc.Save(outputPdfPath);

            Console.WriteLine($"Form fields copied successfully. Output saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
