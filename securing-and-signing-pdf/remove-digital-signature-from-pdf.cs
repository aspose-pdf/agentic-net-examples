using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath  = "signed.pdf";
        const string outputPath = "unsigned.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document
            using (Document doc = new Document(inputPath))
            {
                // Ensure the document contains a form with fields
                if (doc.Form != null && doc.Form.Count > 0)
                {
                    // Gather the names of all signature fields
                    List<string> signatureFieldNames = new List<string>();
                    foreach (Field field in doc.Form)
                    {
                        if (field is SignatureField)
                        {
                            signatureFieldNames.Add(field.FullName);
                        }
                    }

                    // Attempt to delete each signature field
                    foreach (string fieldName in signatureFieldNames)
                    {
                        try
                        {
                            doc.Form.Delete(fieldName);
                            Console.WriteLine($"Removed signature field: {fieldName}");
                        }
                        catch (Exception ex)
                        {
                            Console.Error.WriteLine($"Failed to remove field '{fieldName}': {ex.Message}");
                        }
                    }
                }
                else
                {
                    Console.WriteLine("No form fields found in the document.");
                }

                // Save the modified PDF
                doc.Save(outputPath);
                Console.WriteLine($"Unsigned PDF saved to '{outputPath}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing PDF: {ex.Message}");
        }
    }
}