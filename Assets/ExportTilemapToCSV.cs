using System;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.IO;
using System.Text;

public class ExportTilemapToCSV : MonoBehaviour
{
    public Tilemap tilemap; // Inspectorからアサインする

    private void Start()
    {
        //ExportToCSV();
    }

    // CSV出力をトリガーするメソッド
    public void ExportToCSV()
    {
        StringBuilder csv = new StringBuilder();

        BoundsInt bounds = tilemap.cellBounds;
        TileBase[] allTiles = tilemap.GetTilesBlock(bounds);

        for (int y = bounds.yMax - 1; y >= bounds.yMin; y--)
        {
            for (int x = bounds.xMin; x < bounds.xMax; x++)
            {
                TileBase tile = tilemap.GetTile(new Vector3Int(x, y, 0));
                if (tile != null)
                {
                    csv.Append(tile.name); // タイル名を使用
                }
                else
                {
                    csv.Append("Empty"); // タイルがない場所は"Empty"として出力
                }
                
                if (x < bounds.xMax - 1)
                    csv.Append(","); // セルの区切りとしてカンマを追加
            }
            csv.AppendLine(); // 行の終わり
        }

        string filePath = Path.Combine(Application.dataPath, "ExportedTilemap.csv");
        File.WriteAllText(filePath, csv.ToString());
        Debug.Log("Tilemap exported to CSV at: " + filePath);
    }
}
