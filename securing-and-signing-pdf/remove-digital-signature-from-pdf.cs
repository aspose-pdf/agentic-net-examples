using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "signed_input.pdf";
        const string outputPath = "unsigned_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            using (Document doc = new Document(inputPath))
            {
                var form = doc.Form;
                // Use LINQ Count() because Fields is an IEnumerable, not a property with Count
                if (form == null || form.Fields == null || form.Fields.Count() == 0)
                {
                    Console.WriteLine("No form fields found; nothing to remove.");
                }
                else
                {
                    var signatureFieldNames = new List<string>();
                    foreach (var field in form.Fields)
                    {
                        if (field is SignatureField sigField)
                        {
                            // FullName may be null; fall back to Name
                            string name = sigField.FullName ?? sigField.Name;
                            signatureFieldNames.Add(name);
                        }
                    }

                    foreach (var name in signatureFieldNames)
                    {
                        try
                        {
                            // Correct way to delete a field from the AcroForm
                            form.Delete(name);
                            Console.WriteLine($"Removed signature field: {name}");
                        }
                        catch (Exception ex)
                        {
                            Console.Error.WriteLine($"Could not remove field '{name}': {ex.Message}");
                        }
                    }
                }

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
