using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

/// <summary>
/// タイルマップをcsvに書き込む
/// csvをマップに生成する
/// </summary>
public class MapEditor : EditorWindow
{
    private Tilemap _tileMap;
    private GameObject _parentObject; // 親オブジェクトの参照
    private string _mapDataFileName; // ここにはマップデータのファイル名を入力

    [MenuItem("MyTools/Map Generator")]
    public static void ShowWindow()
    {
        // ウィンドウを表示
        GetWindow<MapEditor>("Map Generator");
    }

    void OnGUI()
    {
        GUILayout.Label("TileMap to CSV", EditorStyles.boldLabel);
        _tileMap = (Tilemap)EditorGUILayout.ObjectField("Tile Map", _tileMap, typeof(Tilemap), true);
        if (GUILayout.Button("Generate CSV"))
        {
            GenerateCSV();
        }
        
        
        GUILayout.Label("Map Generator Settings", EditorStyles.boldLabel);
        // 親オブジェクトフィールド
        _parentObject =
            (GameObject)EditorGUILayout.ObjectField("Parent Object", _parentObject, typeof(GameObject), true);
        _mapDataFileName = EditorGUILayout.TextField("Map Data File Name", _mapDataFileName);

        if (GUILayout.Button("Generate Map"))
        {
            GenerateMap();
        }
    }
    
    /// <summary>
    /// タイルマップを基にCSVを生成
    /// </summary>
    private void GenerateCSV()
    {
        StringBuilder csv = new StringBuilder();

        BoundsInt bounds = _tileMap.cellBounds;
        TileBase[] allTiles = _tileMap.GetTilesBlock(bounds);

        for (int y = bounds.yMax - 1; y >= bounds.yMin; y--)
        {
            for (int x = bounds.xMin; x < bounds.xMax; x++)
            {
                TileBase tile = _tileMap.GetTile(new Vector3Int(x, y, 0));
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
    
    /// <summary>
    /// mapデータファイルを指定して生成
    /// </summary>
    /// <param name="fileName"></param>
    private void GenerateMap()
    {
        // 親オブジェクトが指定されている場合、それを親として設定
        if (_parentObject != null)
        {
            LoadAndInstantiateTiles(_parentObject.transform);
        }

        // ここでマップデータの読み込みと生成のロジックを実行
        // 例: CSVからデータを読み込んでマップを構築
        Debug.Log("Map generated using data from file: " + _mapDataFileName);
        // 実際のマップ生成ロジックをここに実装
    }
    
    /// <summary>
    /// CSVを基にマップを生成
    /// </summary>
    /// <param name="tilesParent"></param>
    private void LoadAndInstantiateTiles(Transform tilesParent)
    {
        //csvを読み取る
        TextAsset data = Resources.Load<TextAsset>("MapTextDatas/" + _mapDataFileName);
        if (data == null)
        {
            Debug.LogWarning("text data is null");
            return;
        }
        string[] lines = data.text.Split('\n');
        
        //スプライトロード
        string basePath = "Map/Terrain (16x16)"; // ベースパス
        Sprite[] sprites = Resources.LoadAll<Sprite>(basePath);
        
        if (sprites.Length > 0)
        {
            for (int y = 0; y < lines.Length; y++)
            {
                string[] cells = lines[y].Split(',');
                for (int x = 0; x < cells.Length; x++)
                {
                    string tileName = cells[x].Trim();
                    if (tileName != "Empty")
                    {
                        //指定スプライトを探す
                        Sprite specificSprite = System.Array.Find(sprites, item => item.name == tileName);
                        if (specificSprite != null)
                        {
                            //ブロック生成する
                            var tilePrefab = new GameObject(tileName);
                            var sr = tilePrefab.AddComponent<SpriteRenderer>();
                            sr.sprite = specificSprite;
                            
                            tilePrefab.transform.SetParent(_parentObject.transform);
                            tilePrefab.transform.position = new Vector3(x - 12.5f, -y + 6.5f, 0);
                        }
                        else
                        {
                            Debug.LogError("Sprite not found for name: " + tileName);
                        }
                    }
                }
            }
        }
        else
        {
            Debug.LogError("No sprites found at path: " + basePath);
        }
    }
}