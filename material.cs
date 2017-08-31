using System;

public class material
{
	public static int librarySize;
	private string materialName;
	private float co1;
	private float co2;
	private float co3;
	private float co4;
	private float co5;
	private float co6;

	public material(string mat, float co1i, float co2i, float co3i,
	                float co4i, float co5i, float co6i){
		materialName = mat;
		co1 = co1i;
		co2 = co2i;
		co3 = co3i;
		co4 = co4i;
		co5 = co5i;
		co6 = co6i;

		librarySize++;
	}


	public string GetMaterial(){
		return materialName;
	}
	public int GetSize(){
		return librarySize;
	}
	public float GetCo1(){
		return co1;
	}
	public float GetCo2(){
		return co2;
	}
	public float GetCo3(){
		return co3;
	}
	public float GetCo4(){
		return co4;
	}
	public float GetCo5(){
		return co5;
	}
	public float GetCo6(){
		return co6;
	}

	public float GetCo(int index)
	{
		float nope = 0;
		if(index == 0){
			return co1;
		}
		if(index == 1){
			return co2;
		}
		if(index == 2){
			return co3;
		}
		if(index == 3){
			return co4;
		}
		if(index == 4){
			return co5;
		}
		if(index == 5){
			return co6;
		}

		else{
			return nope;
		}
/*
		switch(index){
		case 0: return co1; break;
		case 1:
				return co2; break;
		case 2:
				return co3; break;
		case 3:
				return co4; break;
		case 4:
				return co5; break;
		case 5:
				return co6; break;
		case 6:
				return co7; break;
		case 7:
				return co8; break;
		case 8:
				return co9; break;
		case 9:
				return co10; break;
		default: return index;break;
		}*/
	}

	public void SetMaterial(string newMat){
		materialName = newMat;
	}
	public void SetCo1(float co1i){
		co1 = co1i;
	}
	public void SetCo2(float co2i){
		co2 = co2i;
	}


}

	
