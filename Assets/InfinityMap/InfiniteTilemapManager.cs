using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteTilemap : MonoBehaviour
{
	public GameObject player; // 플레이어 오브젝트
	public GameObject[] tilemaps; // 3x3 타일맵 오브젝트 배열
	public float tilemapSize; // 타일맵 하나의 크기

	private Vector3 previousPlayerPosition; // 이전 프레임에서의 플레이어 위치
	private Vector3[] originalPositions; // 각 타일맵의 원래 위치

	void Start()
	{
		previousPlayerPosition = player.transform.position; // 플레이어의 초기 위치 설정
		originalPositions = new Vector3[tilemaps.Length]; // 타일맵의 원래 위치를 저장할 배열 초기화

		// 각 타일맵의 원래 위치 저장
		for (int i = 0; i < tilemaps.Length; i++)
		{
			originalPositions[i] = tilemaps[i].transform.position;
		}
	}

	void Update()
	{
		Vector3 playerPosition = player.transform.position; // 현재 플레이어 위치
		Vector3 moveDirection = playerPosition - previousPlayerPosition; // 이전 위치와의 차이로 이동 방향 계산

		// 플레이어가 타일맵 크기의 절반 이상 이동했을 때 타일맵을 이동
		if (moveDirection.magnitude > tilemapSize / 2)
		{
			Vector3 direction = new Vector3(
				Mathf.Round(moveDirection.x / tilemapSize), // X축 방향으로 이동한 타일의 수
				Mathf.Round(moveDirection.y / tilemapSize), // Y축 방향으로 이동한 타일의 수
				0
			);

			MoveTilemaps(direction); // 타일맵 이동
			previousPlayerPosition = player.transform.position; // 이전 플레이어 위치 업데이트
		}
	}

	void MoveTilemaps(Vector3 direction)
	{
		// 각 타일맵을 방향에 따라 이동
		for (int i = 0; i < tilemaps.Length; i++)
		{
			tilemaps[i].transform.position += direction * tilemapSize;
		}

		RepositionTilemaps(); // 타일맵 재배치
	}

	void RepositionTilemaps()
	{
		// 각 타일맵의 위치를 재조정하여 플레이어 주위에 배치
		foreach (GameObject tilemap in tilemaps)
		{
			Vector3 offset = tilemap.transform.position - player.transform.position;

			// 타일맵이 오른쪽으로 너무 멀리 이동했을 때 왼쪽으로 이동
			if (offset.x > tilemapSize * 1.5f)
			{
				tilemap.transform.position -= new Vector3(tilemapSize * 3, 0, 0);
			}
			// 타일맵이 왼쪽으로 너무 멀리 이동했을 때 오른쪽으로 이동
			else if (offset.x < -tilemapSize * 1.5f)
			{
				tilemap.transform.position += new Vector3(tilemapSize * 3, 0, 0);
			}

			// 타일맵이 위쪽으로 너무 멀리 이동했을 때 아래로 이동
			if (offset.y > tilemapSize * 1.5f)
			{
				tilemap.transform.position -= new Vector3(0, tilemapSize * 3, 0);
			}
			// 타일맵이 아래로 너무 멀리 이동했을 때 위로 이동
			else if (offset.y < -tilemapSize * 1.5f)
			{
				tilemap.transform.position += new Vector3(0, tilemapSize * 3, 0);
			}
		}
	}
}
