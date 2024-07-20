using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteTilemap : MonoBehaviour
{
	public GameObject player; // �÷��̾� ������Ʈ
	public GameObject[] tilemaps; // 3x3 Ÿ�ϸ� ������Ʈ �迭
	public float tilemapSize; // Ÿ�ϸ� �ϳ��� ũ��

	private Vector3 previousPlayerPosition; // ���� �����ӿ����� �÷��̾� ��ġ
	private Vector3[] originalPositions; // �� Ÿ�ϸ��� ���� ��ġ

	void Start()
	{
		previousPlayerPosition = player.transform.position; // �÷��̾��� �ʱ� ��ġ ����
		originalPositions = new Vector3[tilemaps.Length]; // Ÿ�ϸ��� ���� ��ġ�� ������ �迭 �ʱ�ȭ

		// �� Ÿ�ϸ��� ���� ��ġ ����
		for (int i = 0; i < tilemaps.Length; i++)
		{
			originalPositions[i] = tilemaps[i].transform.position;
		}
	}

	void Update()
	{
		Vector3 playerPosition = player.transform.position; // ���� �÷��̾� ��ġ
		Vector3 moveDirection = playerPosition - previousPlayerPosition; // ���� ��ġ���� ���̷� �̵� ���� ���

		// �÷��̾ Ÿ�ϸ� ũ���� ���� �̻� �̵����� �� Ÿ�ϸ��� �̵�
		if (moveDirection.magnitude > tilemapSize / 2)
		{
			Vector3 direction = new Vector3(
				Mathf.Round(moveDirection.x / tilemapSize), // X�� �������� �̵��� Ÿ���� ��
				Mathf.Round(moveDirection.y / tilemapSize), // Y�� �������� �̵��� Ÿ���� ��
				0
			);

			MoveTilemaps(direction); // Ÿ�ϸ� �̵�
			previousPlayerPosition = player.transform.position; // ���� �÷��̾� ��ġ ������Ʈ
		}
	}

	void MoveTilemaps(Vector3 direction)
	{
		// �� Ÿ�ϸ��� ���⿡ ���� �̵�
		for (int i = 0; i < tilemaps.Length; i++)
		{
			tilemaps[i].transform.position += direction * tilemapSize;
		}

		RepositionTilemaps(); // Ÿ�ϸ� ���ġ
	}

	void RepositionTilemaps()
	{
		// �� Ÿ�ϸ��� ��ġ�� �������Ͽ� �÷��̾� ������ ��ġ
		foreach (GameObject tilemap in tilemaps)
		{
			Vector3 offset = tilemap.transform.position - player.transform.position;

			// Ÿ�ϸ��� ���������� �ʹ� �ָ� �̵����� �� �������� �̵�
			if (offset.x > tilemapSize * 1.5f)
			{
				tilemap.transform.position -= new Vector3(tilemapSize * 3, 0, 0);
			}
			// Ÿ�ϸ��� �������� �ʹ� �ָ� �̵����� �� ���������� �̵�
			else if (offset.x < -tilemapSize * 1.5f)
			{
				tilemap.transform.position += new Vector3(tilemapSize * 3, 0, 0);
			}

			// Ÿ�ϸ��� �������� �ʹ� �ָ� �̵����� �� �Ʒ��� �̵�
			if (offset.y > tilemapSize * 1.5f)
			{
				tilemap.transform.position -= new Vector3(0, tilemapSize * 3, 0);
			}
			// Ÿ�ϸ��� �Ʒ��� �ʹ� �ָ� �̵����� �� ���� �̵�
			else if (offset.y < -tilemapSize * 1.5f)
			{
				tilemap.transform.position += new Vector3(0, tilemapSize * 3, 0);
			}
		}
	}
}
