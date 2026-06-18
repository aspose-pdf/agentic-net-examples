using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Folder containing PDFs to process
        const string inputFolder = @"C:\PdfFolder";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Folder not found: {inputFolder}");
            return;
        }

        // Process each PDF file in the folder
        foreach (string pdfPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            try
            {
                // Load the PDF document
                using (Document doc = new Document(pdfPath))
                {
                    // Ensure the document contains a form
                    if (doc.Form != null && doc.Form.Count > 0)
                    {
                        // Retrieve the field named "Comments" and cast to Field safely
                        Field? field = doc.Form["Comments"] as Field;
                        if (field != null && field is TextBoxField textBox)
                        {
                            // Set maximum length to 100 characters
                            textBox.MaxLen = 100;
                            Console.WriteLine($"Updated 'Comments' field in: {Path.GetFileName(pdfPath)}");
                        }
                        else
                        {
                            Console.WriteLine($"'Comments' field not found or not a TextBoxField in: {Path.GetFileName(pdfPath)}");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"No form found in: {Path.GetFileName(pdfPath)}");
                    }

                    // Save changes back to the same file
                    doc.Save(pdfPath);
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
            }
        }

        Console.WriteLine("Batch processing completed.");
    }
}