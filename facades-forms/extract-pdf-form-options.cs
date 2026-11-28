using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string pdfPath = "FormD.pdf";
        const string fieldName = "SurveyQ";
        const string outputPath = "SurveyQOptions.txt";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {pdfPath}");
            return;
        }

        try
        {
            // Load the PDF document.
            using (Document doc = new Document(pdfPath))
            {
                // Find the field by its full name (or partial name).
                var field = doc.Form?.Fields?.FirstOrDefault(f =>
                    string.Equals(f.FullName, fieldName, StringComparison.Ordinal) ||
                    string.Equals(f.Name, fieldName, StringComparison.Ordinal));

                if (field == null)
                {
                    Console.Error.WriteLine($"Field '{fieldName}' not found in the PDF.");
                    return;
                }

                // The selectable options are available only on ListBoxField or ComboBoxField.
                string[] options = null;
                if (field is ListBoxField listBox)
                {
                    options = listBox.Options?.Select(o => o?.ToString()).Where(o => o != null).ToArray();
                }
                else if (field is ComboBoxField comboBox)
                {
                    options = comboBox.Options?.Select(o => o?.ToString()).Where(o => o != null).ToArray();
                }
                else
                {
                    Console.Error.WriteLine($"Field '{fieldName}' is not a list or combo box and has no selectable options.");
                    return;
                }

                // Write the options to the output text file, one per line.
                File.WriteAllLines(outputPath, options ?? Array.Empty<string>());
            }

            Console.WriteLine($"Options for field '{fieldName}' have been written to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
