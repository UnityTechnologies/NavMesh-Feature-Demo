using UnityEngine;
using System.Collections;

public class Maze
{
	public int m_depth = 20;
	public int m_width = 20;
	public bool[,] v_wall;
	public bool[,] h_wall;
	public int[,] cell;
	public bool[,] floor;
	public int m_nb_holes = 2;
	public int m_nb_holes_in_walls;

	public Maze(int depth, int width, int nb_holes, int nb_holes_in_walls)
	{
		m_depth = depth;
		m_width = width;
		m_nb_holes = nb_holes;
		m_nb_holes_in_walls = nb_holes_in_walls;
		v_wall = new bool[m_depth,m_width + 1]; //  Z , X
		h_wall = new bool[m_depth + 1, m_width]; //  Z , X
		cell = new int[m_depth, m_width]; //  Z , X
		floor = new bool[m_depth, m_width]; //  Z , X
		for (int i = 0; i < v_wall.GetLength(0); i++){
			for (int j = 0; j < v_wall.GetLength(1); j++)
				v_wall[i,j] = true;
		}
		for (int i = 0; i < h_wall.GetLength(0); i++){
			for (int j = 0; j < h_wall.GetLength(1); j++)
				h_wall[i,j] = true;
		}
		for (int i = 0; i < m_depth; i++){
			for (int j = 0; j < m_width; j++){
				cell[i,j] = m_width * i + j;
				floor[i,j] = true;
			}
		}
	}
	
	void replace_ids(int[] repl)
	{
		for (int d = 0; d < m_depth; d++)
		{
			for (int w = 0; w < m_width; w++)
			{
				if (cell[d,w] == repl[1])
					cell[d,w] = repl[0];
			}
		}
	}
	
	bool ids_all_zero(int[] repl)
	{
		if (repl[0] != 0 && repl[0] != 0)
			return false;
		for (int k = 0; k < m_depth; k++){
			for (int j = 0; j < m_width; j++){
				if (cell[k,j] != 0)
					return false;
			}
		}
		return true;
	}
	
	public void generate(bool rand_exits, bool rand_floor_exits)
	{
		int i = 0;
		
		while (++i > -1)
		{
			int[] repl;

			if (Random.Range(0, 2) >= 1)
			{
				int Z = Random.Range(0, v_wall.GetLength(0));
				int X = Random.Range(1, v_wall.GetLength(1) - 1);
				if (cell[Z,X] == cell[Z,X - 1])
					continue;
				repl = new int[] { cell[Z,X], cell[Z,X - 1] };
				v_wall[Z,X] = false;
			}
			else
			{
				int Z = Random.Range(1, h_wall.GetLength(0) - 1);
				int X = Random.Range(0, h_wall.GetLength(1));
				if (cell[Z,X] == cell[Z - 1,X])
					continue;
				repl = new int[] { cell[Z,X], cell[Z - 1,X] };
				h_wall[Z,X] = false;
			}
			System.Array.Sort(repl);
			replace_ids(repl);
			if (ids_all_zero(repl) == true)
				break;
		}
		create_exits(rand_exits, rand_floor_exits);
	}
	
	void create_exits(bool rand_exits, bool rand_floor_exits)
	{
		int i = 0;
		int depth = 0;
		int width = 0;
		
		for (i = 0; i < m_nb_holes && rand_floor_exits; i++){
			int d = Random.Range(0, m_depth);
			int w = Random.Range(0, m_width);
			if (floor[d,w] == false && m_nb_holes < floor.GetLength(0) * floor.GetLength(1))
				i--;
			floor[d,w] = false;
		}
		for (i = 0; i < m_nb_holes_in_walls && rand_exits; i++){
			if (Random.Range(0, 2) == 1)
			{
				depth = Random.Range(0, v_wall.GetLength(0));
				if (width == 0)
					width = 1;
				else width = 0;
				width *= (v_wall.GetLength(1) - 1);
				if (v_wall[depth, width] == false && m_nb_holes_in_walls < v_wall.GetLength(0) * 2 + h_wall.GetLength(1) * 2)
					i--;
				v_wall[depth, width] = false;
			}
			else
			{
				width = Random.Range(0, h_wall.GetLength(1));
				if (depth == 0)
					depth = 1;
				else depth = 0;
				depth *= (h_wall.GetLength(0) - 1);
				if (h_wall[depth, width] == false && m_nb_holes_in_walls < v_wall.GetLength(0) * 2 + h_wall.GetLength(1) * 2)
					i--;
				h_wall[depth, width] = false;
			}
		}
		if (!rand_exits && m_nb_holes_in_walls > 0)
			h_wall[0,0] = false;
		if (!rand_exits && m_nb_holes_in_walls > 1)
			h_wall[h_wall.GetLength(0) - 1, h_wall.GetLength(1) - 1] = false;
		if (!rand_floor_exits && m_nb_holes_in_walls > 0)
			floor[0,0] = false;
		if (!rand_floor_exits && m_nb_holes_in_walls > 1)
			floor[floor.GetLength(0) - 1, floor.GetLength(1) - 1] = false;
	}
}

public class Maze3d
{
	public Maze[] maze;
	public int m_height;
	public int m_width;
	public int m_depth;
	public bool[,] roof;
	public int m_nb_holes;
	public int m_nb_holes_in_walls;

	public Maze3d(int height, int depth, int width, int nb_holes, int nb_holes_in_walls)
	{
		m_nb_holes = nb_holes;
		m_nb_holes_in_walls = nb_holes_in_walls;
		roof = new bool[depth, width];
		m_height = height;
		m_depth = depth;
		m_width = width;
		maze = new Maze[height];
	}
	
	public void generate(bool rand_exits, bool rand_floor_exits)
	{
		for (int i = 0; i < m_height; i++){
			maze[i] = new Maze(m_depth, m_width, m_nb_holes, m_nb_holes_in_walls);
			maze[i].generate(rand_exits, rand_floor_exits);
		}
		for (int w = 0; w < m_depth; w++){
			for (int d = 0; d < m_width; d++)
				roof[w,d] = true;
		}
	}
}

[ExecuteInEditMode]
[DefaultExecutionOrder(-200)]
public class maze_generation : MonoBehaviour {
	
	public GameObject horizontalWall;
	public GameObject verticalWall;
	public GameObject corner;
	public bool generateCorners = true;
	public bool generateFloors = true;
	public bool optimiseCorners = true;
	public GameObject floor;
	public int width = 10;
	public int depth = 10;
	public int heigth = 5;
	public float wallHeight = 3;
	public float floorSize = 3;
	public Maze3d m_maze3d;
	public bool roof = false;
	public int exitsInFloor = 2;
	public int exitsInWalls = 2;
	public bool randFloorExits = true;
	public bool randWallsExits = true;
	public bool generateAtAwake = true;
	float dim;
	float div_dim;
	GameObject keyHolder;

	// Use this for initialization
	void Start ()
	{
		if (generateAtAwake && Application.isPlaying)
			generate();
	}

	public void clean()
	{
		foreach (Transform t in transform.GetComponentsInChildren<Transform>())
		{
			try
			{
				if (t != this.transform)
					GameObject.DestroyImmediate(t.gameObject);
			}
			catch {}
		}
	}

	public void generate()
	{
		dim = floorSize;
		div_dim = dim / 2.0F;
		m_maze3d = new Maze3d(heigth, depth, width, exitsInFloor, exitsInWalls);
		m_maze3d.generate(randWallsExits, randFloorExits);
		pop_maze_3d(m_maze3d, new Vector3(0, 0, 0));
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void pop_maze_part(GameObject obj, Vector3 pos, Quaternion quat)
	{
		GameObject tmp = Instantiate(obj, pos, quat) as GameObject;
		tmp.transform.parent = this.gameObject.transform;
	}
	
	void pop_maze_2d(Maze maze, Vector3 pos)
	{
		for (int z = 0; z < maze.h_wall.GetLength(0); z++){
			for (int x = 0; x < maze.h_wall.GetLength(1); x++){
				if (maze.h_wall[z,x])
					pop_maze_part(horizontalWall, new Vector3(x * dim + div_dim + pos.x, pos.y + wallHeight / 2.0F, z * dim + pos.z), Quaternion.identity);
			}
		}
		for (int z = 0; z < maze.v_wall.GetLength(0); z++)
		{
			for (int x = 0; x < maze.v_wall.GetLength(1); x++)
			{
				if (maze.v_wall[z,x])
					pop_maze_part(verticalWall, new Vector3(x * dim + pos.x, pos.y + wallHeight / 2.0F, z * dim + div_dim + pos.z), Quaternion.identity);
			}
		}
	}
	
	void pop_floor(Maze maze, Vector3 pos)
	{
		for (int z = 0; z < maze.floor.GetLength(0) && generateFloors; z++){
			for (int x = 0; x < maze.floor.GetLength(1); x++){
				if (maze.floor[z,x])
					pop_maze_part(floor, new Vector3(x * dim + div_dim + pos.x, pos.y, z * dim + div_dim + pos.z), Quaternion.identity);					
			}
		}
	}
	
	void pop_corner(Maze maze, Vector3 pos)
	{
		for (int z = 1; z < maze.floor.GetLength(0) && generateCorners; z++)
		{
			for (int x = 1; x < maze.floor.GetLength(1); x++)
			{
				if (optimiseCorners == true && x < maze.floor.GetLength(1) && z < maze.floor.GetLength(0))
				{
					if (x == 0 || (maze.h_wall[z, x] && maze.h_wall[z, x - 1]))
						continue;
					if (z == 0 || (maze.v_wall[z, x] && maze.v_wall[z - 1, x]))
						continue;
				}
				pop_maze_part(corner, new Vector3(x * dim + pos.x, pos.y + wallHeight / 2, z * dim + pos.z), Quaternion.identity);									
			}
		}
	}
	
	void pop_maze_3d(Maze3d maze3d, Vector3 pos)
	{
		keyHolder = new GameObject("keyHolder");
		keyHolder.transform.parent = this.transform;
		for (int i = 0; i < maze3d.m_height; i++)
		{
			pop_maze_2d(maze3d.maze[i], pos);
			pop_floor(maze3d.maze[i], pos);
			pop_corner(maze3d.maze[i], pos);
			pos.y += wallHeight;
		}
		for (int z = 0; z < maze3d.roof.GetLength(0) && roof; z++){
			for (int x = 0; x < maze3d.roof.GetLength(1); x++)
				pop_maze_part(floor, new Vector3(x * dim + div_dim + pos.x, pos.y, z * dim + div_dim + pos.z), Quaternion.identity);					
		}
		generate_keys();
	}

	void generate_keys()
	{
		for (int i = 0; i < heigth; i++)
		{
			for (int z = 0; z < depth; z++)
			{
				for (int x = 0; x < width; x++)
				{
					if (m_maze3d.maze[i].floor[z,x] == false)
						createKey("floorHole lvl" + heigth, i, z, x);
				}
				if (m_maze3d.maze[i].v_wall[z, 0] == false)
					createKey("west exit lvl" + heigth, i, z, 0);
				if (m_maze3d.maze[i].v_wall[z, width] == false)
					createKey("east exit lvl" + heigth, i, z, width);
			}
			for (int x = 0; x < width; x++)
			{
				if (m_maze3d.maze[i].h_wall[0, x] == false)
					createKey("south exit lvl" + heigth, i, 0, x);
				if (m_maze3d.maze[i].h_wall[depth, x] == false)
					createKey("north exit lvl" + heigth, i, depth, x);
			}
		}
	}
	
	void createKey(string name, int i, int z, int x)
	{
		GameObject tmp = new GameObject(name);
		tmp.transform.parent = keyHolder.transform;
		tmp.transform.localPosition = new Vector3(x * floorSize, i * wallHeight, z * floorSize);
	}
	
	public float get_dim()
	{
		return dim;
	}

	public float get_div_dim()
	{
		return div_dim;
	}

}
