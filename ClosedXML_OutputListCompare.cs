using System;
using System.Collections.Generic;
using System.Linq;
using ClosedXML.Excel;

public class TsTable
{
    public int No { get; set; }
    public List<string> LocNo { get; set; }
}

class Program
{
    static void Main()
    {
        // TsTableクラスのインスタンスを2つ作成します
        TsTable tsTable1 = new TsTable
        {
            No = 1,
            LocNo = new List<string> { "Loc1", "Loc2", "Loc3", "Loc4", "Loc5" }
        };

        TsTable tsTable2 = new TsTable
        {
            No = 2,
            LocNo = new List<string> { "Loc3", "Loc4", "Loc5", "Loc6", "Loc7" }
        };

        // TsTableクラスのインスタンスをリストに追加します
        List<TsTable> tsTableList = new List<TsTable> { tsTable1, tsTable2 };

        // 重複なしのリストを作成し、ソートします
        List<string> mrglist = tsTable1.LocNo.Union(tsTable2.LocNo).OrderBy(x => x).ToList();

        // ClosedXMLを使用してExcelに出力します
        using (var workbook = new XLWorkbook())
        {
            var worksheet = workbook.Worksheets.Add("Sheet1");

            for (int i = 0; i < mrglist.Count; i++)
            {
                worksheet.Cell(1, i + 2).Value = tsTable1.LocNo.Contains(mrglist[i]) ? mrglist[i] : "";
                worksheet.Cell(2, i + 2).Value = tsTable2.LocNo.Contains(mrglist[i]) ? mrglist[i] : "";
            }

            workbook.SaveAs("output.xlsx");
        }
    }
}