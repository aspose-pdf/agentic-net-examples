using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";          // PDF containing the form
        const string dropdownFieldName = "Language";      // Name of the dropdown list field
        const string outputTxtPath = "languages.txt";     // Output text file

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        try
        {
            // Form (not FormEditor) is used for reading existing form data.
            using (var form = new Form())
            {
                // Bind the PDF document to the Form facade.
                form.BindPdf(inputPdfPath);

                // Retrieve the facade for the specified dropdown field.
                FormFieldFacade fieldFacade = form.GetFieldFacade(dropdownFieldName);

                if (fieldFacade == null)
                {
                    Console.WriteLine($"Field '{dropdownFieldName}' not found in the PDF.");
                }
                else
                {
                    // The Items property contains the list of display values for ComboBox/ListBox fields.
                    string[] listItems = fieldFacade.Items;

                    if (listItems == null || listItems.Length == 0)
                    {
                        Console.WriteLine($"No items found in dropdown field '{dropdownFieldName}'.");
                    }
                    else
                    {
                        // Write each item to a new line in the output text file.
                        File.WriteAllLines(outputTxtPath, listItems);
                        Console.WriteLine($"Exported {listItems.Length} language items to '{outputTxtPath}'.");
                    }
                }

                // Unbind/close the PDF – optional because the using block disposes the object.
                form.Close();
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
