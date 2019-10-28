using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class FastListBenchmark : MonoBehaviour {
	void Update() {
		if (Input.GetKeyDown(KeyCode.Space)) {
			float arrayTime = 0f;
			float listTime = 0f;
			float fastListTime = 0f;

			for (int j=0; j<100; j++) {
				Stopwatch timer = new Stopwatch();
				timer.Start();
				int[] array = new int[1000000];
				for (int i=0;i<1000000;i++) {
					array[i] = i;
				}
				for (int i=0; i<999999; i++) {
					array[i] = array[i+1];
				}
				timer.Stop();
				arrayTime += timer.ElapsedMilliseconds;

				timer.Reset();

				timer.Start();
				List<int> list = new List<int>(1000000);
				for (int i=0;i<1000000;i++) {
					list.Add(i);
				}
				for (int i=0; i<999999; i++) {
					list[i] = list[i+1];
				}
				timer.Stop();
				listTime += timer.ElapsedMilliseconds;

				timer.Reset();

				timer.Start();
				FastList<int> fastList = new FastList<int>(1000000);
				for (int i=0;i<1000000;i++) {
					fastList[i] = i;
				}
				fastList.count=1000000;
				for (int i=0; i<999999; i++) {
					fastList[i] = fastList[i+1];
				}
				timer.Stop();
				fastListTime += timer.ElapsedMilliseconds;
				
				timer.Reset();
			}

			arrayTime /= 100f;
			listTime /= 100f;
			fastListTime /= 100f;
			
			UnityEngine.Debug.Log($"T[] - {arrayTime}ms, List<T> - {listTime}ms, FastList<T> - {fastListTime}ms");
		}
	}
}
