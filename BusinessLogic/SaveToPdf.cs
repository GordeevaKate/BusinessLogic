
using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
using MigraDoc.Rendering;
using System.Collections.Generic;
using BusinessLogic.HelperModels;
using BusinessLogic.BindingModel;
using System;

namespace BusinessLogic.Report
{
    public class SaveToPdf
    {
        public static void CreateDoc(PdfInfo info)
        {

            Document document = new Document();
            DefineStyles(document);
            Section section = document.AddSection();
            Paragraph paragraph = section.AddParagraph(info.Title);
            paragraph.Format.SpaceAfter = "1cm";
            paragraph.Format.Alignment = ParagraphAlignment.Center;
            paragraph.Style = "NormalTitle";
            paragraph.Style = "Normal";
            var table = document.LastSection.AddTable();
            List<string> columns = new List<string> { "3cm", "4cm", "6cm", "4cm" };

            foreach (var elem in columns)
            {
                table.AddColumn(elem);
            }
            CreateRow(new PdfRowParameters
            {
                Table = table,
                Texts =info.Colon,
                Style = "NormalTitle",
                ParagraphAlignment = ParagraphAlignment.Center
            });
            foreach (var sf in info.Clients)
            {
                CreateRow(new PdfRowParameters
                {
                    Table = table,
                    Texts = new List<string>
                    {
                        sf.Pasport,
                          sf.ClientFIO,
                          sf.PhoneNumber,
                          sf.Email
                     
                    },
                    Style = "Normal",
                    ParagraphAlignment = ParagraphAlignment.Left
                });
            }
            PdfDocumentRenderer renderer = new PdfDocumentRenderer(true, PdfSharp.Pdf.PdfFontEmbedding.Always)
            {
                Document = document
            };
            renderer.RenderDocument();
            renderer.PdfDocument.Save(info.FileName);
        }
        private static void DefineStyles(Document document)
        {
            Style style = document.Styles["Normal"];
            style.Font.Name = "Times New Roman";
            style.Font.Size = 14;
            style = document.Styles.AddStyle("NormalTitle", "Normal");
            style.Font.Bold = true;
        }
        private static void CreateRow(PdfRowParameters rowParameters)
        {
            Row row = rowParameters.Table.AddRow();
            for (int i = 0; i < rowParameters.Texts.Count; ++i)
            {
                FillCell(new PdfCellParameters
                {
                    Cell = row.Cells[i],
                    Text = rowParameters.Texts[i],
                    Style = rowParameters.Style,
                    BorderWidth = 0.5,
                    ParagraphAlignment = rowParameters.ParagraphAlignment
                });
            }
        }
        private static void FillCell(PdfCellParameters cellParameters)
        {
            cellParameters.Cell.AddParagraph(cellParameters.Text);
            if (!string.IsNullOrEmpty(cellParameters.Style))
            {
                cellParameters.Cell.Style = cellParameters.Style;
            }
            cellParameters.Cell.Borders.Left.Width = cellParameters.BorderWidth;
            cellParameters.Cell.Borders.Right.Width = cellParameters.BorderWidth;
            cellParameters.Cell.Borders.Top.Width = cellParameters.BorderWidth;
            cellParameters.Cell.Borders.Bottom.Width = cellParameters.BorderWidth;
            cellParameters.Cell.Format.Alignment = cellParameters.ParagraphAlignment;
            cellParameters.Cell.VerticalAlignment = VerticalAlignment.Center;
        }























        public static void CreateDocDogovor(PdfInfo info) { 

            Document document = new Document();
            DefineStyles(document);
            Section section = document.AddSection();
            Paragraph paragraph = section.AddParagraph(info.Title);
          
            paragraph.Format.SpaceAfter = "1cm";
            paragraph.Format.Alignment = ParagraphAlignment.Center;
            paragraph.Style = "NormalTitle";
            paragraph.Style = "Normal";
            paragraph = section.AddParagraph($"{info.dogovor.data}");
            paragraph.Format.SpaceAfter = "1cm";
            paragraph.Format.Alignment = ParagraphAlignment.Right;
            paragraph.Style = "NormalTitle";
            paragraph.Style = "Normal";
            paragraph = section.AddParagraph($"Заказчик {info.Client} и Агент частного агенства {info.Agent} заключили договор о следующих перевозках");
            paragraph.Format.SpaceAfter = "1cm";
            paragraph.Format.Alignment = ParagraphAlignment.Left;
            paragraph.Style = "NormalTitle";
            paragraph.Style = "Normal";
            var table = document.LastSection.AddTable();
            List<string> columns = new List<string> { "3cm", "2cm", "3cm", "3cm", "3cm", "3cm", "2cm" };

            foreach (var elem in columns)
            {
                table.AddColumn(elem);
            }
            CreateRow(new PdfRowParameters
            {
                Table = table,
                Texts = info.Colon,
                Style = "NormalTitle",
                ParagraphAlignment = ParagraphAlignment.Center
            });
            foreach (var dr in info.dogovor_Reis)
            {
                if(dr.DogovorId==info.dogovor.Id)
                foreach(var reis in info.reiss) {
                    if (dr.ReisId == reis.Id)
                    {
                        foreach(var raion1 in info.raion)
                        {
                            foreach (var raion2 in info.raion)
                            {
                                if ((reis.OfId==raion2.Id)&&(reis.ToId==raion1.Id))
                                {
                                    CreateRow(new PdfRowParameters
                                    {
                                        Table = table,
                                        Texts = new List<string>
                                          {
                                            reis.Name,
                                            Convert.ToString(dr.NadbavkaCena+dr.ves*reis.Cena),
                                            raion2.Name,
                                             raion1.Name,
                                            Convert.ToString(reis.Time+dr.NadbavkaTime)+ "дня",
                                           Convert.ToString(dr.Obem),
                                             Convert.ToString(dr.ves),
                                         },
                                        Style = "Normal",
                                        ParagraphAlignment = ParagraphAlignment.Left
                                    });
                                }
                            }
                        }
                    }
                
                }
                
            }
            paragraph = section.AddParagraph($"На общуйю сумму:{info.dogovor.Summa}");
            paragraph.Format.SpaceAfter = "1cm";
            paragraph.Format.Alignment = ParagraphAlignment.Left;
            paragraph.Style = "NormalTitle";
            paragraph.Style = "Normal";
            paragraph = section.AddParagraph($"Подпись исполнителя:");
            paragraph.Format.SpaceAfter = "1cm";
            paragraph.Format.Alignment = ParagraphAlignment.Left;
            paragraph.Style = "NormalTitle";
            paragraph.Style = "Normal"; paragraph = section.AddParagraph($"Подпись заказчика:");
            paragraph.Format.SpaceAfter = "1cm";
            paragraph.Format.Alignment = ParagraphAlignment.Left;
            paragraph.Style = "NormalTitle";
            paragraph.Style = "Normal";

            PdfDocumentRenderer renderer = new PdfDocumentRenderer(true, PdfSharp.Pdf.PdfFontEmbedding.Always)
            {
                Document = document
            };
            renderer.RenderDocument();
            renderer.PdfDocument.Save(info.FileName);
        }
    }
}
