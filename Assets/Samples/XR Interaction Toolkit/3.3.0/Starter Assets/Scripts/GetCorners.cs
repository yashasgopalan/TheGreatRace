using UnityEngine;

public class MazeCornerFinder : MonoBehaviour
{
    [Header("Maze Settings")]
    public string mazeName = "Maze_new";
    public bool showCornerMarkers = true;
    public bool printToConsole = true;

    // Player origin (this script MUST be attached to the player)
    private Vector3 playerStartPosition;

    void Start()
    {
        // Lock player spawn as origin (0,0,0)
        playerStartPosition = transform.position;

        Debug.Log($"Player origin locked at spawn: {playerStartPosition}");
        Debug.Log("Player coordinate system initialized → spawn = (0,0,0)");

        FindMazeCorners();
    }

    void FindMazeCorners()
    {
        GameObject maze = GameObject.Find(mazeName);

        if (maze == null)
        {
            Debug.LogError($"Maze '{mazeName}' not found!");
            return;
        }

        Renderer mazeRenderer = maze.GetComponent<Renderer>();

        if (mazeRenderer == null)
        {
            Debug.LogError($"Maze '{mazeName}' has no Renderer!");
            return;
        }

        Bounds bounds = mazeRenderer.bounds;

        // World-space maze corners
        Vector3 c1 = new Vector3(bounds.min.x, 0, bounds.min.z);
        Vector3 c2 = new Vector3(bounds.max.x, 0, bounds.min.z);
        Vector3 c3 = new Vector3(bounds.max.x, 0, bounds.max.z);
        Vector3 c4 = new Vector3(bounds.min.x, 0, bounds.max.z);

        // Convert to player-relative coordinates (player spawn = origin)
        Vector3 r1 = c1 - playerStartPosition;
        Vector3 r2 = c2 - playerStartPosition;
        Vector3 r3 = c3 - playerStartPosition;
        Vector3 r4 = c4 - playerStartPosition;

        if (printToConsole)
        {
            Debug.Log("═══ MAZE CORNERS (PLAYER SPAWN = 0,0,0) ═══");

            Debug.Log($"Corner 1: ({r1.x:F2}, {r1.z:F2})");
            Debug.Log($"Corner 2: ({r2.x:F2}, {r2.z:F2})");
            Debug.Log($"Corner 3: ({r3.x:F2}, {r3.z:F2})");
            Debug.Log($"Corner 4: ({r4.x:F2}, {r4.z:F2})");

            Debug.Log("═══════════════════════════════════════════");
        }

        if (showCornerMarkers)
        {
            CreateCornerMarker(c1, Color.blue, "Corner 1");
            CreateCornerMarker(c2, Color.green, "Corner 2");
            CreateCornerMarker(c3, Color.yellow, "Corner 3");
            CreateCornerMarker(c4, Color.magenta, "Corner 4");

            Vector3 center = new Vector3(bounds.center.x, 0, bounds.center.z);
            CreateCornerMarker(center, Color.cyan, "Maze Center");
        }
    }

    void CreateCornerMarker(Vector3 position, Color color, string label)
    {
        GameObject marker = GameObject.CreatePrimitive(PrimitiveType.Cube);
        marker.transform.position = position;
        marker.transform.localScale = Vector3.one * 0.5f;
        marker.GetComponent<Renderer>().material.color = color;
        marker.name = label;
    }
}
