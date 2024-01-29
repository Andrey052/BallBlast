using UnityEngine;

public enum EdgeType
{
	Left, Right, Bottom
}

public class LevelEdge : MonoBehaviour
{
	[SerializeField] private EdgeType type;

	public EdgeType Type => type;
}