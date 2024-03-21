# Vertical 2D Shooting Game Project

* 대학 졸업 과제
* 제작기간 : 2020년 8월 21일 → 2020년 11월 17일

## 개요
* 접근이 쉽고 조작하기 편한 안드로이드 플랫폼을 활용하여 종스크롤 슈팅게임을 제작

## 라이브러리
* Unity
* C#

## 기능
* 가비지 컬렉션(Garbage Collection)의 발생으로 랙(Lag)이 발생하는 것을 막기 위해 오브젝트 풀링(Object Pooling)을 사용.
* Stage를 Text파일 형식으로 작성하고 게임을 시작하면 읽어들여 Enemy를 소환한다.
  * Enemy를 소환하기 위해 필요한 요소들을 소환하는 시간, 타입, 소환 위치로 분류하고 구조체로 정리.

### 참조
* [골드메탈 스튜디오 강좌](https://blog.naver.com/gold_metal/221703991554)
