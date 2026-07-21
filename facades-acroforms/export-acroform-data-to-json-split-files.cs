using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Facades;               // Form facade resides here
using Aspose.Pdf.Forms;                // Form field classes reside here
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

// Alias the ambiguous Form class from the Facades namespace
using PdfFormFacade = Aspose.Pdf.Facades.Form;

class ExportFormData
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";               // source PDF with AcroForm
        const string outputFolder = "JsonParts";               // folder for split JSON files
        const int maxFieldsPerFile = 100;                      // limit per file

        // ---------------------------------------------------------------------
        // 1. Create a minimal PDF that contains a form field so the example can run
        //    in a sandbox where no external files exist.
        // ---------------------------------------------------------------------
        CreateSamplePdfWithForm(inputPdfPath);

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Load the PDF form using the Form facade (fully qualified via alias)
        using (PdfFormFacade form = new PdfFormFacade(inputPdfPath))
        {
            // Export all form fields to a JSON stream (indented for readability)
            using (MemoryStream jsonStream = new MemoryStream())
            {
                form.ExportJson(jsonStream, true);
                jsonStream.Position = 0; // rewind for reading

                // Read the entire JSON content as a string
                string fullJson = new StreamReader(jsonStream).ReadToEnd();

                // Parse the JSON – ExportJson returns a JSON array of field objects
                JArray allFields = JArray.Parse(fullJson);

                // Split the array into chunks of at most maxFieldsPerFile items
                int fileIndex = 1;
                for (int i = 0; i < allFields.Count; i += maxFieldsPerFile)
                {
                    JArray chunk = new JArray(
                        allFields.Skip(i).Take(maxFieldsPerFile)
                    );

                    // Serialize the chunk back to JSON (indented)
                    string chunkJson = chunk.ToString(Formatting.Indented);

                    // Write the chunk to a separate file
                    string outPath = Path.Combine(
                        outputFolder,
                        $"fields_part{fileIndex}.json"
                    );

                    File.WriteAllText(outPath, chunkJson);
                    fileIndex++;
                }
            }
        }

        Console.WriteLine("Form data exported and split into JSON files successfully.");
    }

    /// <summary>
    /// Generates a simple PDF containing an AcroForm with a single text box field.
    /// This method is required because the execution sandbox does not provide any
    /// pre‑existing files.
    /// </summary>
    private static void CreateSamplePdfWithForm(string path)
    {
        // Create a new empty document and add a page
        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();

            // Create a text box field, set a name and a default value
            TextBoxField txtField = new TextBoxField(page, new Rectangle(100, 600, 300, 650))
            {
                PartialName = "SampleText",
                Value = "Hello, Aspose!"
            };

            // Add the field to the document's form collection
            doc.Form.Add(txtField, 1);

            // Save the document so the Form facade can later open it
            doc.Save(path);
        }
    }
}
