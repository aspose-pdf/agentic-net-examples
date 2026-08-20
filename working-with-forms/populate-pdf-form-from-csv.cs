using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string pdfTemplatePath = "template.pdf";   // PDF with form fields
        const string csvDataPath     = "data.csv";       // CSV file: first line = header (field names), second line = values
        const string outputPdfPath   = "filled_output.pdf";

        if (!File.Exists(pdfTemplatePath))
        {
            Console.Error.WriteLine($"PDF template not found: {pdfTemplatePath}");
            return;
        }

        if (!File.Exists(csvDataPath))
        {
            Console.Error.WriteLine($"CSV data file not found: {csvDataPath}");
            return;
        }

        try
        {
            // Load the PDF document (lifecycle rule: use Document constructor)
            using (Document pdfDoc = new Document(pdfTemplatePath))
            {
                // Read CSV file
                using (StreamReader reader = new StreamReader(csvDataPath))
                {
                    // Parse header (field names)
                    string headerLine = reader.ReadLine();
                    if (headerLine == null)
                    {
                        Console.Error.WriteLine("CSV file is empty.");
                        return;
                    }

                    string[] fieldNames = SplitCsvLine(headerLine);

                    // Read first data row (you can extend this to process multiple rows)
                    string dataLine = reader.ReadLine();
                    if (dataLine == null)
                    {
                        Console.Error.WriteLine("CSV file contains no data rows.");
                        return;
                    }

                    string[] fieldValues = SplitCsvLine(dataLine);

                    // Ensure header and data column counts match
                    int columns = Math.Min(fieldNames.Length, fieldValues.Length);

                    for (int i = 0; i < columns; i++)
                    {
                        string fieldName = fieldNames[i].Trim();
                        string fieldValue = fieldValues[i].Trim();

                        // Try to locate the form field by its full name
                        if (pdfDoc.Form.HasField(fieldName))
                        {
                            // Retrieve the generic field object
                            var field = pdfDoc.Form[fieldName];

                            // Set the value based on the concrete field type
                            switch (field)
                            {
                                case TextBoxField txt:
                                    txt.Value = fieldValue;
                                    break;
                                case CheckboxField chk:
                                    // Accept "true", "1", "yes" as checked
                                    chk.Checked = fieldValue.Equals("true", StringComparison.OrdinalIgnoreCase) ||
                                                   fieldValue.Equals("1") ||
                                                   fieldValue.Equals("yes", StringComparison.OrdinalIgnoreCase);
                                    break;
                                case ComboBoxField combo:
                                    combo.Value = fieldValue;
                                    break;
                                case ListBoxField list:
                                    list.Value = fieldValue;
                                    break;
                                case RadioButtonField radio:
                                    // For radio buttons the Value property holds the selected option
                                    radio.Value = fieldValue;
                                    break;
                                default:
                                    Console.WriteLine($"Warning: field \"{fieldName}\" exists but its type is not handled.");
                                    break;
                            }
                        }
                        else
                        {
                            // Field not found – optionally log or ignore
                            Console.WriteLine($"Warning: field \"{fieldName}\" not found in PDF form.");
                        }
                    }
                }

                // Save the populated PDF (lifecycle rule: use Document.Save)
                pdfDoc.Save(outputPdfPath);
                Console.WriteLine($"PDF saved to '{outputPdfPath}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }

    // Simple CSV splitter handling commas; does not handle quoted commas for brevity
    static string[] SplitCsvLine(string line)
    {
        return line.Split(new[] { ',' }, StringSplitOptions.None);
    }
}
