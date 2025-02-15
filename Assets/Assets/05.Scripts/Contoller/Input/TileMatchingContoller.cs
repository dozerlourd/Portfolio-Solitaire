using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMatchingContoller : MonoBehaviour
{

    [SerializeField] ControllerManagementSystem controllerManagementSystem;
    public GameObject[,] board;  // 패를 관리하는 2D 배열

    private int[] dx = { 0, 0, 1, -1 }; // 우, 좌, 하, 상 이동
    private int[] dy = { 1, -1, 0, 0 };

    private class Node
    {
        public int x, y, turn, dir;
        public Node(int x, int y, int turn, int dir)
        {
            this.x = x;
            this.y = y;
            this.turn = turn;
            this.dir = dir;
        }
    }

    public bool CanConnect(Vector2Int start, Vector2Int end)
    {
        print(board[start.x, start.y].name);
        print(board[end.x, end.y].name);

        // 패가 존재하는지 체크
        if (board[start.x, start.y] == null || board[end.x, end.y] == null) return false;
        if (board[start.x, start.y].name != board[end.x, end.y].name) return false; // 같은 패인지 체크

        Queue<Node> queue = new Queue<Node>();
        bool[,,] visited = new bool[controllerManagementSystem.TileGenerateContoller.Rows, controllerManagementSystem.TileGenerateContoller.Cols, 4];

        // 4방향 초기 탐색
        for (int i = 0; i < 4; i++)
        {
            int nx = start.x + dx[i];
            int ny = start.y + dy[i];
            if (IsValid(nx, ny, end))
            {
                queue.Enqueue(new Node(nx, ny, 0, i));
                visited[nx, ny, i] = true;
            }
        }

        while (queue.Count > 0)
        {
            Node node = queue.Dequeue();

            // 도착 지점 도달 & 꺾은 횟수 3 이하
            if (node.x == end.x && node.y == end.y && node.turn <= 2)
            {
                return true;
            }

            for (int i = 0; i < 4; i++)
            {
                int nx = node.x + dx[i];
                int ny = node.y + dy[i];
                int newTurn = (i == node.dir) ? node.turn : node.turn + 1;

                if (IsValid(nx, ny, end) && newTurn <= 2 && !visited[nx, ny, i])
                {
                    queue.Enqueue(new Node(nx, ny, newTurn, i));
                    visited[nx, ny, i] = true;
                }
            }
        }

        return false;
    }

    private bool IsValid(int x, int y, Vector2Int end)
    {
        // 범위 체크
        if (x < 0 || x >= controllerManagementSystem.TileGenerateContoller.Rows || y < 0 || y >= controllerManagementSystem.TileGenerateContoller.Cols)
            return false;

        // 빈칸이거나 도착 지점일 경우 이동 가능
        return board[x, y] == null || (x == end.x && y == end.y);
    }
}
